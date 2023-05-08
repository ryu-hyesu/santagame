using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    private Dictionary<string,Ink.Runtime.Object> variables;

    public DialogueVariables(string globalsFilePath){
        string inkFileContens = File.ReadAllText(globalsFilePath);
        Ink.Compiler complier = new Ink.Compiler(inkFileContens);
        Story globalVariableStroy = complier.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariableStroy.variablesState){
            Ink.Runtime.Object value = globalVariableStroy.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("dd" + name + value);
        }
    }
    public void StartListening(Story story){
        VariableToStory(story);
        story.variablesState.variableChangedEvent += variableChanged;
    }

    public void StopListening(Story story){
        story.variablesState.variableChangedEvent -= variableChanged;
    }



    private void variableChanged(string name, Ink.Runtime.Object value){
        if(variables.ContainsKey(name)){
            variables.Remove(name);
            variables.Add(name,value);
        }
    }

    private void VariableToStory(Story story){
        foreach(KeyValuePair<string,Ink.Runtime.Object> variable in variables){
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
