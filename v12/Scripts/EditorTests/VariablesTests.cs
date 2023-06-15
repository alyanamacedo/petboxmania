using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class VariablesTests
{
    /* _Gherkin_
    #language: pt

    Funcionalidade: Manter as variáveis e constantes globais ativas ao mudar de cena

    Cenário: Identificar as cenas
        Dado: as cenas que estão no build
        Quando: ao inicializar o sistema
        Então: lista com o nome das cenas com quantidade igual a 3
    */
    [Test]
    public void SceneNames_Test(){
        //ARRANGE
        //var variables = new Variables(); // error CS0246: The type or namespace name 'Variables' could not be found (are you missing a using directive or an assembly reference?)
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int count = 3;

        //ACT
        var sceneNames = SceneNames();

        //ASSERT
        Assert.That(sceneCount, Is.EqualTo(sceneNames.Count));
        Assert.That(sceneCount, Is.EqualTo(count));
        for (int i = 0; i < sceneCount; i++)
        {
            string expectedSceneName = SceneUtility.GetScenePathByBuildIndex(i);
            expectedSceneName = System.IO.Path.GetFileNameWithoutExtension(expectedSceneName);
            Assert.That(expectedSceneName, Is.EqualTo(sceneNames[i]).IgnoreCase);
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    public List<string> SceneNames(){
        List<string> scenes = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            scenes.Add(sceneName);
        }

        return scenes;
    } 
}
