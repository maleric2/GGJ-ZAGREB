using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.View
{
    class MenuManager
    {
        #region Constants

        public const string SCENE_MENU = "MainMenu";
        public const string SCENE_PLAY = "Play";
        public const string SCENE_CONTINUEPLay = "Continue";
        public const string SCENE_ = "Play";

        #endregion

        public void OnPlayButton()
        {

            SceneManager.LoadScene(SCENE_PLAY);

        }
        

    }
}
