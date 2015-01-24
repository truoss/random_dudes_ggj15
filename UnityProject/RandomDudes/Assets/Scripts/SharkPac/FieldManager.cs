using UnityEngine;
using SharkPac;
using System.Collections;
using System.Collections.Generic;

namespace SharkPac
{
    public class FieldManager : MonoBehaviour
    {
        public static FieldManager I;
        public BasePlayer sharkPlayer;
        public BasePlayer dudePlayer;
        public Target target;
        public Obstacle[] obstaclePrefabs;

        //public int xCount = 18;
        //public int yCount = 10;
        public Field[,] fields = new Field[18, 10];
        public List<Obstacle> obstacles = new List<Obstacle>();

        void Awake()
        {
            I = this;
        }

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var field = transform.GetChild(i).GetComponent<Field>() as Field;
                var _name = field.name.Split('_');
                fields[int.Parse(_name[0]), int.Parse(_name[1])] = field;
            }

            InitFields();
        }


        [ContextMenu("GenerateField")]
        public void GenerateField()
        {
            DeleteAllFields();
            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    if (fields[x, y] == null)
                        fields[x, y] = CreateField(x, y);
                }
            }
        }

        public Field CreateField(int x, int y)
        {
            var obj = new GameObject(x.ToString() + "_" + y.ToString());
            obj.AddComponent<Field>();
            obj.transform.parent = this.transform;
            var _field = obj.GetComponent<Field>();

            return _field;
        }

        [ContextMenu("DeleteAllFields")]
        public void DeleteAllFields()
        {
            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    if (fields[x, y] != null)
                    {
                        DestroyImmediate(fields[x, y].gameObject);
                        fields[x, y] = null;
                    }
                }
            }
        }

        [ContextMenu("InitFields")]
        public void InitFields()
        {
            ResetFields();

            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    if (fields[x, y] != null)
                    {
                        switch (fields[x, y].currentState)
                        {
                            case Field.FieldState.FREE:
                                break;
                            case Field.FieldState.BLOCKED:
                                SetBlock(x, y);
                                break;
                            case Field.FieldState.PLAYER1START:
                                SetPlayerPosition(dudePlayer, x, y);
                                break;
                            case Field.FieldState.PLAYER2START:
                                SetPlayerPosition(sharkPlayer, x, y);
                                break;
                            case Field.FieldState.TARGET:
                                SetTarget(x, y);
                                break;
                            default:
                                break;
                        }
                        //field[x, y].currentState
                    }
                    else
                        Debug.LogError("Could not init fields", this);
                }
            }
        }

        private void SetTarget(int x, int y)
        {
            Debug.LogWarning("SetTarget: " + x + "," + y, this);
            target.transform.position = new Vector3(x, y, 0);
        }

        public void SetPlayerPosition(BasePlayer player, int x, int y)
        {
            //Debug.LogWarning("SetPlayerPosition: " + x + "," + y, this);
            
            player.xCoord = x;
            player.yCoord = y;

            //player.transform.position = new Vector3(x, y, 0);           
        }

        public bool CheckIfMovePossible(BasePlayer player, int x, int y)
        {
            var newX = player.xCoord + x;
            var newY = player.yCoord + y;

           // Debug.LogWarning("CheckIfMovePossible: " + newX + "," + newY, this);
            if (newX >= fields.GetLength(0) || newX < 0 || newY >= fields.GetLength(1) || newY < 0)
                return false;
            else if (fields[newX, newY].currentState == Field.FieldState.BLOCKED)
                return false;
            else
                return true;
        }

        private void SetBlock(int x, int y)
        {
            Debug.LogWarning("SetBlock: " + x + "," + y, this);
            var obj = Instantiate(obstaclePrefabs[Random.Range(0,(obstaclePrefabs.Length - 1))], new Vector3(x, y, 0), Quaternion.identity) as Obstacle;
            obj.transform.parent = fields[x, y].transform;
            obstacles.Add(obj);
        }

        [ContextMenu("ResetBlocks")]
        public void ResetFields()
        {
            var _tmp = obstacles.ToArray();
            for (int i = 0; i < obstacles.Count; i++)
            {
                DestroyImmediate(_tmp[i].gameObject);
            }

            obstacles.Clear();
        }
    }
}
