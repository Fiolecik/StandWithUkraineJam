
using System.Collections;
using System.Collections.Generic;
using Game;
using Team;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitControllerShower : MonoBehaviour
{
    private TeamController lastMoved;
    [SerializeField] private GameObject imageSketch;
    [SerializeField] private float cardXOsfset;
    [SerializeField] private float cardYOffset;

    private List<UIUnitSketch> baseCards = new List<UIUnitSketch>();
    private List<UIUnitSketch> unitCards = new List<UIUnitSketch>();

    private int baseCount = 0;
    private int notBaseCount = 0;

    void Update()
    {
        var now = TourController.Instance.CurrentMoving;
        if (now != lastMoved || now.GetComponent<UnitController>().Cards.Count!=baseCount  || now.SpawnedUnit.GetComponent<UnitController>().Cards.Count!=notBaseCount)
        {
            baseCount = now.GetComponent<UnitController>().Cards.Count;
            notBaseCount = now.SpawnedUnit.GetComponent<UnitController>().Cards.Count;
            lastMoved = now;
            ChangeMoved();
        }
    }

    private void ChangeMoved()
    {
        MakeArrayFromData(ref baseCards, lastMoved.GetComponent<UnitController>(), Vector3.up*cardYOffset);
        MakeArrayFromData(ref unitCards, lastMoved.SpawnedUnit.GetComponent<UnitController>(), Vector3.zero);
    }

    private void MakeArrayFromData(ref List<UIUnitSketch> array, UnitController uc, Vector3 pos)
    {
        for (int i = 0; i < array.Count; i++)
        {
            Destroy(array[i].gameObject);
        }
        array.Clear();

        for (int i = 0; i < uc.Cards.Count; i++)
        {
            GameObject g = Instantiate(imageSketch, transform);
            var xd = g.GetComponent<UIUnitSketch>();
            xd.transform.position = pos + transform.position + Vector3.right * cardXOsfset;
            xd.SetUnit(uc.Cards[i]);
            array.Add(xd);
        }
    }
}
