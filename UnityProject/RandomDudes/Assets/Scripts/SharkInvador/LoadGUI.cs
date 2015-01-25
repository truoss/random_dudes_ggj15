using UnityEngine;
using System.Collections;

namespace SharkInvador
{
    public class LoadGUI : MonoBehaviour
    {
        void Awake()
        {
            Application.LoadLevelAdditive(1);
        }

        void Start()
        {
            MainUI.I.SetLeftCharacter(MainUI.CharacterState.SHARK);
            MainUI.I.SetRightCharacter(MainUI.CharacterState.SHARK);
        }
    }
}
