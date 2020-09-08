using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneChange : MonoBehaviour
{
    private IEnumerator coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coroutine == null)
        {
            coroutine = corou();
            StartCoroutine(coroutine);
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        }
    }

    IEnumerator corou()
    {
        var op = SceneManager.UnloadSceneAsync("SampleScene");
        yield return op;

        yield return Resources.UnloadUnusedAssets();
    }
}
