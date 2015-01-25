using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SharkJumper
{

[System.Serializable()]
public class PlayerSettings
{
    public List<Sprite> JumpUpLeftSprites = new List<Sprite>();
    public List<Sprite> JumpUpRightSprites = new List<Sprite>();
    public List<Sprite> FallDownLeftSprites = new List<Sprite>();
    public List<Sprite> FallDownRightSprites = new List<Sprite>();
    public Sprite ApexSprite;

    public Sprite DefaultSprite;

    public float JumpHeight = 10;
    public float MoveSpeed = 10;

    public float GravityScale = 1;

    public float RayCastDepth = 3;

    public float MaxAdditionalJumpTimer = 0.3f;
    public float MaxAddJumpHeight = 3;

    public float VelocityUpThresholdForJumpUpAnim = 15;
    public float VelocityDownThresholdForFallAnim = -15;

    public void Initialize()
    {

    }
}

[System.Serializable()]
public class CameraSettings
{
    public float CameraScrollSpeed = 1;
    public float CameraDistance = 25;
    public float IncreasementofScrollSpeed = 1.01f;
    public float BorderDistanceBeforeCameraStartsMoving = 5;

    private Camera ourCam;
    private Transform camTransform;

    public float CurrentLowestYPosition = -1000;
    public float CurrentHighestYPosition = 10;

    public void Initialize(SharkJump Owner)
    {
        SharkPlatformProxy[] tempList = GameObject.FindObjectsOfType<SharkPlatformProxy>();
        for (int i = 0, iMax = tempList.Length; i < iMax; i++)
        {
            if (tempList[i].transform.position.y > CurrentHighestYPosition) CurrentHighestYPosition = tempList[i].transform.position.y;
        }
        GameObject tempGO = new GameObject("Camera", typeof(Camera));
        ourCam = tempGO.GetComponent<Camera>();
        ourCam.orthographic = true;
        ourCam.orthographicSize = CameraDistance;
        camTransform = tempGO.GetComponent<Transform>();
        camTransform.position = new Vector3(Owner.levelSettings.PlatformGenerationBounds.x * 0.72f, CameraDistance*0.64f, -5);
    }

    public void Update()
    {
        camTransform.Translate(Vector3.up * Time.deltaTime * CameraScrollSpeed);

        CameraScrollSpeed *= IncreasementofScrollSpeed;
        CurrentLowestYPosition = ourCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }
}

[System.Serializable()]
public class LevelSettings
{
    public List<GameObject> PlatformPrefabs = new List<GameObject>();

    public int AmountOfPlatforms = 10;

    public Vector2 PlatformGenerationBounds = new Vector2(80, 800);

    public float StartPositionOffsetHeight = 5;


    public Vector2 AmountModifierRandomMin = new Vector2(0.7f, 0.7f);
    public Vector2 AmountModifier = new Vector2(0.3f, 1f);

    public Vector2 PlatformDistanceModifier = new Vector2(1, 1);
    public Vector2 MinPlatformDistanceModifier = new Vector2(0.1f, 0.1f);

    public List<Color> RandomPlatformColors = new List<Color>(new Color[1] { new Color(1, 1, 1) });

    public List<SharkPlatformProxy> StartPlatforms = new List<SharkPlatformProxy>();

    public void Initialize(SharkJump Owner)
    {
        int PlatformPrefabCounts = PlatformPrefabs.Count;
        int PlatformColorCounts = RandomPlatformColors.Count;
        Vector2 placingDistance = new Vector2();

        float Aspect = 1;
        int AmountOfPlatformsX = AmountOfPlatforms, AmountOfPlatformsY= AmountOfPlatforms;

        if(PlatformGenerationBounds.x > PlatformGenerationBounds.y)
        {
            Aspect = PlatformGenerationBounds.x / PlatformGenerationBounds.y;

            AmountOfPlatformsY = Mathf.Max(1, (int)(AmountOfPlatforms / Aspect * AmountModifier.y));
            AmountOfPlatformsX = (int)((AmountOfPlatforms - AmountOfPlatformsY) * AmountModifier.x);
        }
        else
        {
            Aspect = PlatformGenerationBounds.y / PlatformGenerationBounds.x;

            AmountOfPlatformsX = Mathf.Max(1, (int)(AmountOfPlatforms / Aspect * AmountModifier.x));
            AmountOfPlatformsY = (int)((AmountOfPlatforms - AmountOfPlatformsX) * AmountModifier.y);
        }

        Debug.Log("Amount X: " + AmountOfPlatformsX + " ,Amount Y: " + AmountOfPlatformsY);
        placingDistance.x = PlatformGenerationBounds.x / AmountOfPlatformsX * PlatformDistanceModifier.x;
        placingDistance.y = Mathf.Min(PlatformGenerationBounds.y / AmountOfPlatformsY, (Owner.playerSettings.JumpHeight + Owner.playerSettings.MaxAdditionalJumpTimer) / (Owner.playerSettings.GravityScale*40)) * PlatformDistanceModifier.y;


        float CurrentPosX = 0, CurrentPosY = 0;
        for (int y = 0, yMax = (int)(AmountOfPlatformsY * AmountModifierRandomMin.y); y < yMax; y++)
        {

            for (int x = 0, xMax = (int)(AmountOfPlatformsX * AmountModifierRandomMin.x); x < xMax; x++)
            {
                

                CurrentPosX += Random.Range(MinPlatformDistanceModifier.x * placingDistance.x, placingDistance.x);
                

                GameObject tempGO = GameObject.Instantiate(PlatformPrefabs[Random.Range(0, PlatformPrefabCounts)],new Vector3(CurrentPosX,CurrentPosY,0),Quaternion.identity) as GameObject;
                SharkPlatformProxy tempPlatform = tempGO.GetComponent<SharkPlatformProxy>();
                tempPlatform.Color = RandomPlatformColors[Random.Range(0, PlatformColorCounts)];
                if (CurrentPosX > PlatformGenerationBounds.x) CurrentPosX = 0;

                if (StartPlatforms.Count < Owner.PlayerCount && Random.Range(0,2) == 0) StartPlatforms.Add(tempPlatform);
            }
            CurrentPosY += Random.Range(MinPlatformDistanceModifier.y * placingDistance.y, placingDistance.y);
        }

        
        
       
    }
}

public class SharkJump : MonoBehaviour 
{
    public int PlayerCount = 0;

    public static SharkJump I;
    public PlayerSettings playerSettings = new PlayerSettings();
    public CameraSettings cameraSettings = new CameraSettings();
    public LevelSettings levelSettings = new LevelSettings();

    public static  List<PlayerProxy> players = new List<PlayerProxy>();

    void Awake()
    {
        I = this;
        //Load GUI
        Application.LoadLevelAdditive(1);
    }

    void Start()
    {
        players.Clear();
        Random.seed = (int)System.DateTime.Now.Ticks;
        playerSettings.Initialize();
        levelSettings.Initialize(this);
        cameraSettings.Initialize(this);

        for (int i = 0, iMax = PlayerCount; i < iMax; i++)
        {
            PlayerProxy tempPlayer = new PlayerProxy();
            players.Add(tempPlayer);
            tempPlayer.Initialize(this, i);
        }

        //Set Gui
        MainUI.I.SetLeftCharacter(MainUI.CharacterState.DUDE);
        MainUI.I.SetRightCharacter(MainUI.CharacterState.DUDE);
    }


    // Update is called once per frame
    void Update () {

        for (int i = 0, iMax = players.Count; i < iMax; i++)
        {
            players[i].Update();
        }

        cameraSettings.Update();
    }

    public IEnumerator Wait(int p, UnityAction action)
    {
        yield return new WaitForSeconds(p);

        action();
    }
}

public class PlayerProxy
{
    private SpriteRenderer _sprite;
    private Collider2D _collider;
    private Rigidbody2D _rigid;
    public Transform _myTransform;    

    private SharkJump Owner;

    private int ID;

    private float JumpPressedTimer = 0;
    private bool _JumpPressed = false;
    private bool NewJumpPressed = false;
    private bool JumpPressed
    {
        get { return _JumpPressed; }
        set
        {
            if (value != _JumpPressed)
            {
                _JumpPressed = value;
                JumpPressedTimer = 0;
            }
        }
    }
    private float _HorizontalSpeed = 0;
    private float HorizontalSpeed
    {
        get { return _HorizontalSpeed; }
        set
        {
            if (value != _HorizontalSpeed)
            {
                _HorizontalSpeed = value;
                if (_HorizontalSpeed > 0)
                {
                    _myTransform.localScale = new Vector3(1, 1, 1);
                }
                else if(_HorizontalSpeed < 0)
                {
                    _myTransform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    //private int CurrentSpriteIndex = 0;

    public void Initialize(SharkJump Owner, int _ID)
    {
        this.Owner = Owner;
        ID = _ID;

        GameObject tempGO = new GameObject("Player[" + _ID + "]");
        tempGO.tag = "Player";
        _sprite = tempGO.AddComponent<SpriteRenderer>();
        _sprite.sprite = Owner.playerSettings.DefaultSprite;
        _sprite.sortingOrder = 100 + _ID;
        _collider = tempGO.AddComponent<CircleCollider2D>();
        _rigid = tempGO.AddComponent<Rigidbody2D>();
        _rigid.fixedAngle = true;
        _rigid.gravityScale = Owner.playerSettings.GravityScale;
        _myTransform = tempGO.GetComponent<Transform>();
        _myTransform.position = Owner.levelSettings.StartPlatforms[_ID].transform.position + new Vector3(0, Owner.levelSettings.StartPositionOffsetHeight, 0);
    }

    private SharkPlatformProxy _LastPlatformProxy;
    public SharkPlatformProxy LastPlatformProxy
    {
        get { return _LastPlatformProxy; }
        set
        {
            if (value != _LastPlatformProxy)
            {
                if(_LastPlatformProxy != null)
                {
                    _LastPlatformProxy.AddRemovePlayer(this);
                }
                _LastPlatformProxy = value;
                if (_LastPlatformProxy != null)
                {
                    _LastPlatformProxy.AddRemovePlayer(this);
                }
            }
        }
    }

    private bool Grounded = false;

    public void Update()
    {
        InputUpdate();

        _rigid.velocity= new Vector2(HorizontalSpeed, _rigid.velocity.y);
        _collider.enabled = _rigid.velocity.y <= 0;
        SharkPlatformProxy tempProxy = null;
        if (_collider.enabled)
        {
            
            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(_myTransform.position.x-0.2f, _myTransform.position.y), new Vector2(0, -1), Owner.playerSettings.RayCastDepth);
            //Debug.DrawRay(_myTransform.position, new Vector3(0, -2, 0));
            for (int i = 0, iMax = hit.Length; i < iMax; i++)
            {
                tempProxy = hit[i].transform.gameObject.GetComponent<SharkPlatformProxy>();
                if (tempProxy != null)
                {
                    LastPlatformProxy = tempProxy;
                    break;
                }
            }

            if (tempProxy == null || hit.Length == 0)
            {
                hit = Physics2D.RaycastAll(new Vector2(_myTransform.position.x + 0.2f, _myTransform.position.y), new Vector2(0, -1), Owner.playerSettings.RayCastDepth);
              //  Debug.DrawRay(_myTransform.position, new Vector3(0, -2, 0));
                for (int i = 0, iMax = hit.Length; i < iMax; i++)
                {
                    tempProxy = hit[i].transform.gameObject.GetComponent<SharkPlatformProxy>();
                    if (tempProxy != null)
                    {
                        LastPlatformProxy = tempProxy;
                        break;
                    }
                }
            }



            if (tempProxy == null || hit.Length == 0) LastPlatformProxy = null;
        }
        Grounded = tempProxy != null;
        if (JumpPressed)
        {
            if (Grounded && NewJumpPressed)
            {
                _rigid.AddForce(new Vector2(0, Owner.playerSettings.JumpHeight));
            }
            else
            {
                JumpPressedTimer += Time.deltaTime;
                if (JumpPressedTimer < Owner.playerSettings.MaxAdditionalJumpTimer)
                    _rigid.AddForce(new Vector2(0, Owner.playerSettings.MaxAddJumpHeight));
                   // _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y+ Owner.playerSettings.MaxAddJumpHeight);
            }
        }

        if (_myTransform.position.y+2 <= Owner.cameraSettings.CurrentLowestYPosition || _myTransform.position.x > Owner.levelSettings.PlatformGenerationBounds.x+20 || _myTransform.position.x < -10)
        {
            Debug.Log("Player[" + ID + "] Lost this Round");
            Owner.enabled = false;

            if (ID == 0)
            {
                MainUI.I.AddRightPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
            }
            else
            {
                MainUI.I.AddLeftPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
            }

            //SetState(GameState.START);
            SharkJump.I.StartCoroutine(SharkJump.I.Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
        }

        if (_myTransform.position.y >= Owner.cameraSettings.CurrentHighestYPosition)
        {
            Debug.Log("Player[" + ID + "] Won this Round");
            Owner.enabled = false;

            if (ID == 0)
            {
                MainUI.I.AddLeftPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
            }
            else
            {
                MainUI.I.AddRightPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
            }


            
            //SetState(GameState.START);
            SharkJump.I.StartCoroutine(SharkJump.I.Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
        }
        AnimationUpdate();
    }
        

    private void AnimationUpdate()
    {
        //Debug.Log( Grounded.ToString());
        if(Grounded)
        {
            if (ID == 0)
                _sprite.sprite = Owner.playerSettings.DefaultSprite;
            else
                _sprite.sprite = Owner.playerSettings.ApexSprite;            
            return;
        }
       

        if (_rigid.velocity.y > Owner.playerSettings.VelocityUpThresholdForJumpUpAnim)
        {
            if(ID == 0)
                _sprite.sprite = Owner.playerSettings.JumpUpLeftSprites[0];
            else
                _sprite.sprite = Owner.playerSettings.JumpUpRightSprites[0];
        }
        else if (_rigid.velocity.y < Owner.playerSettings.VelocityDownThresholdForFallAnim)
        {
            if (ID == 0)
                _sprite.sprite = Owner.playerSettings.FallDownLeftSprites[0];
            else
                _sprite.sprite = Owner.playerSettings.FallDownRightSprites[0];
        }
        else
        {
            _sprite.sprite = Owner.playerSettings.ApexSprite;
        }
    }

    private void InputUpdate()
    {
        NewJumpPressed = false;
        switch (ID)
        {
            case 0:
                HorizontalSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * Owner.playerSettings.MoveSpeed;
                if (Input.GetButton("Fire1"))
                {
                    if (!JumpPressed) NewJumpPressed = true;
                    JumpPressed = true;
                }
                else
                {
                    JumpPressed = false;
                }
                break;
            case 1:
                HorizontalSpeed = Input.GetAxis("Horizontal2") * Time.deltaTime * Owner.playerSettings.MoveSpeed;
                if (Input.GetButton("Fire2"))
                {
                    if (!JumpPressed) NewJumpPressed = true;
                    JumpPressed = true;
                }
                else
                {
                    JumpPressed = false;
                }
                break;
        }
    }


}

}