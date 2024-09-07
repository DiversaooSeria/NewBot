using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;


[CreateAssetMenu(fileName = "Attributes", menuName = "Player/Attributes")]
public class PlayerAttr : ScriptableObject
{
    public float speed;
    public bool mode;
    public Vector2[] points, normalized;
    public EnemyAttr.ShapeType shapeType;


    private void OnEnable()
    {
        shapeType = EnemyAttr.ShapeType.None;
        EnemyAttr.TipoForma += SetTipoForma;
        points = null;
        normalized = null;
    }
    private void OnDisable()
    {
        EnemyAttr.TipoForma -= SetTipoForma;
        points = null;
        normalized = null;
    }


    public void GeneratePoints(EnemyAttr.ShapeType type) // gerando uma lista de pontos baseado no tipo geométrico
    {
        switch (type)
        {
            case EnemyAttr.ShapeType.Square: // tipo quadrado
                points = new Vector2[5];
                break;

            case EnemyAttr.ShapeType.Triangle: // tipo triangulo
                points = new Vector2[4];
                break;
        }
    }
    public void SetTipoForma(EnemyAttr.ShapeType shape)
    {
        shapeType = shape;
        Debug.Log(shape);
        Debug.Log(shapeType);
        GeneratePoints(shapeType);
    }
    public void NormalizesPoints()
    {
        normalized = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            normalized[i] = points[i].normalized;
        }
    }
}

