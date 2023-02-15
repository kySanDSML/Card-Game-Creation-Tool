using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Keyword Aliases", menuName = "KeywordAliases")]
public class KeywordAliases : ScriptableSingleton<KeywordAliases> 
{
//    public HashSet<KeywordStringPair> aliases = new HashSet<KeywordStringPair>();
    private static Dictionary<string, KeywordStringPair> aliases= new Dictionary<string, KeywordStringPair>();

    public static Dictionary<string, KeywordStringPair> getAliases()
    {
        return aliases;
    }

    public static KeywordAliases Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instance = this;
        }

        if (aliases.Count == 0)
        {
            foreach (keywords kw in keywords.GetValues(typeof(keywords)))
            {

                KeywordStringPair kwp = new KeywordStringPair(kw, ObjectNames.NicifyVariableName(kw.ToString()));

                aliases.Add(kwp.KeyWord.ToString(), kwp);
                _aliasList.Add(kwp);
            }
        }

    }

    [SerializeField] private List<KeywordStringPair> _aliasList = new List<KeywordStringPair>();

    private static Dictionary<string, WordStringPair> wordAliases = new Dictionary<string, WordStringPair>();
    [SerializeField] private List<WordStringPair> wordList = new List<WordStringPair>();

    public static string getWordAlias(string word)
    {
        if (wordAliases.ContainsKey(word))
        {
            return wordAliases[word].alias;
        }
        else
        {
            return word;
        }
    }

    public void OnValidate()
    {
        foreach (KeywordStringPair ksp in _aliasList)
        {
            aliases[ksp.KeyWord.ToString()] = ksp;
            //Debug.Log(aliases[ksp.KeyWord.ToString()].KeyWord + " = " + aliases[ksp.KeyWord.ToString()].KeyWordAlias);
        }

        foreach (WordStringPair ksp in wordList)
        {
            wordAliases[ksp.word] = ksp;
            Debug.Log(wordAliases[ksp.word].word + " = " + wordAliases[ksp.word].alias);
        }
        Debug.Log(aliases.Count);
    }
}
