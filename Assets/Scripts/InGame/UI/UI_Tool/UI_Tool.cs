﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_Tool : MonoBehaviour
{
    public Button button;
    private Action action;

    public void SetButtonText(string text) => button.GetComponentInChildren<Text>().text = text;
    public void SetButtonSprite(Sprite sprite) => button.GetComponent<Image>().sprite = sprite;
    public void SetAction(Action action) => this.action = action;

    /// <summary>
    /// Called by the UI_SelectionManager, when a tile is clicked.
    /// This delegate function should be set by the parent of this tool.
    /// </summary>
    public void Action() => action();
    public void OnClick() => UI_SelectionManager.INSTANCE.SetSelection(this);
    public void Select() => SetButtonSelected();
    public void Deselect() => SetButtonDeselected();
    public void SetButtonSelected() => ToggleButtonColors();
    public void SetButtonDeselected() => ToggleButtonColors();

    public void ToggleButtonColors()
    {
        var tempColors = button.colors;
        var temp = tempColors.normalColor;
        tempColors.normalColor = tempColors.selectedColor;
        tempColors.selectedColor = temp;
        temp = tempColors.highlightedColor;
        tempColors.highlightedColor = tempColors.pressedColor;
        tempColors.pressedColor = temp;
        button.colors = tempColors;
    }

    public List<Tile.TileType> GetInvalidTileTypes()
    {
        return new List<Tile.TileType>() { Tile.TileType.Water, Tile.TileType.Road };
    }
}
