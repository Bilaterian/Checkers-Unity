using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update


    public void ChangeToScene(string sceneToChangeTo) {
        Application.LoadLevel(sceneToChangeTo);
    }
}
