﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PanelUnitInfoManager : MonoBehaviour, ISelectionObserver
{
    int unitsLayerMask;
    const int RAY_LIM = 600;
    public Canvas canvas;
    public UI_PanelUnitInfo_Enemy pf_enemyPanel;
    public UI_PanelUnitInfo_Unit pf_unitPanel;
    public UI_PanelUnitInfo_Building pf_buildingPanel;
    UI_PanelUnitInfo panelUnitInfo;
    public IPropertiesDisplayable displaying;
    public static UI_PanelUnitInfoManager INSTANCE;

    void Start()
    {
        INSTANCE = this;

        // panelUnitInfo.gameObject.SetActive(false);
        unitsLayerMask = LayerMask.NameToLayer("Units");
        SelectionManager.INSTANCE.AddListener(SelectionManager.ObserverType.Unit, this);
    }


    public void CloseInfo()
    {
        if (panelUnitInfo != null) 
        {
            Destroy(panelUnitInfo.gameObject);
        }
    }

    public void OnClick(GameObject gameUnit)
    {
        // Summary:
        //     If gameUnit is an implementation of IDisplayable, display
        //     its info using a Panel unit Info
        // 
        //     NOTE: only pass Game Objects under layer "Units" as parameter

        if (gameUnit == null) return; // if no Game Objects under layer "Units" are clicked

        // Show info of game object in unit info panel if the object's properties are displayable
        IPropertiesDisplayable displayable = gameUnit.GetComponent<IPropertiesDisplayable>();
        CloseInfo();
        if (displayable != null)
        {
            ShowInfo(displayable);
        }
    }

    public void ShowInfo(IPropertiesDisplayable displayable)
    {
        // display info of selected unit
        UI_PanelUnitInfo panel = displayable.GetPanelUnitInfo();
        panelUnitInfo = Instantiate(panel, canvas.transform);
        UpdateInfo();
        displaying = displayable;
    }

    /// <summary>
    /// Update info of selected unit
    /// </summary>
    public void UpdateInfo() => panelUnitInfo.UpdateInfo();

    public void OnDisplayableTakeDmg(IPropertiesDisplayable displayable)
    {
        if (displaying == displayable && displayable != null)
        {
            UpdateInfo();
        }
    }

    public void OnGameUnitDie(GameUnit gameUnit)
    {
        if (gameUnit as IPropertiesDisplayable == displaying)
        {
            AttackRangeMarker.Hide();
            CloseInfo();
        }
    }

    void ISelectionObserver.OnSelect(Object obj) {
        OnClick((GameObject)obj);
        AttackRangeMarker.FollowUnit(((GameObject)obj).GetComponent<GameUnit>());
    }

    void ISelectionObserver.OnDeselect(Object obj)
    {
        CloseInfo();
        AttackRangeMarker.Hide();
    }

    void ISelectionObserver.OnMouseDown(Object obj) { }

    void ISelectionObserver.OnMouseUp(Object obj) { }

    void ISelectionObserver.OnMouseEnter(Object obj) { }

    void ISelectionObserver.OnMouseExit(Object obj) { }
}
