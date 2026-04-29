using UnityEngine;

[System.Serializable]
public class PersonSO 
{ 
    public GameObject Mesh { get => _mesh;  }
    public Sprite Picture { get => _picture;  }
    public Gender Gender { get => _gender;  }

    public string Name { get; set; }

    [SerializeField] private GameObject _mesh;
    [SerializeField] private Sprite _picture;
    [SerializeField] private Gender _gender;
}