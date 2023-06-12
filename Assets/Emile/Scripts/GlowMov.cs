using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowMov: MonoBehaviour
{
    Dictionary<Renderer, Material[]> glowMaterialDictionary = new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> originalMaterialDictionary = new Dictionary<Renderer, Material[]>();
    Dictionary<Color, Material> cachedGlowMaterials = new Dictionary<Color, Material>();
    public Material glowMaterial;
    private bool isGlowing = false;
    private Color originalGlowColor;
    private Color validSpaceColor = new(0,0.6f,0);
    private Color poseColor =   new(0,0.4f,0);
    private Color atkColor = new(0.5f, 0, 0);
    
    private void Awake()
    {
        PrepareMaterialDictionaries();
        originalGlowColor = glowMaterial.GetColor("_Color");
        GlwPse();
    }
    private void PrepareMaterialDictionaries()
    {
        GameObject child = transform.GetChild(0).gameObject;
        foreach (Renderer renderer in child.GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if (cachedGlowMaterials.TryGetValue(originalMaterials[i].color, out mat) == false)
                {
                    mat = new Material(glowMaterial);
                    //By default, Unity considers a color with the property name name "_Color" to be the main color
                    mat.color = originalMaterials[i].color;
                    cachedGlowMaterials[mat.color] = mat;
                }
                newMaterials[i] = mat;
            }
            glowMaterialDictionary.Add(renderer, newMaterials);
        }
    }

    internal void HighlightValidPath()
    {
        if (isGlowing == false)
        {
            return;
        }
        foreach (Renderer renderer in glowMaterialDictionary.Keys)
        {
            foreach (Material item in glowMaterialDictionary[renderer])
            {
                item.SetColor("_Color", validSpaceColor);
            }
        }
    }

    internal void ResetGlowHighlight()
    {
        foreach (Renderer renderer in glowMaterialDictionary.Keys)
        {
            foreach (Material item in glowMaterialDictionary[renderer])
            {
                item.SetColor("_Color", originalGlowColor);

            }
        }
    }

    public void ToggleGlow()
    {
        if (isGlowing == false)
        { 
            ResetGlowHighlight();
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterialDictionary[renderer];
            }

        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing = !isGlowing;
    }
    public void ToggleGlow(bool state)
    {
        if (isGlowing == state)
            return;
        isGlowing = !state;
        ToggleGlow();
    }
    public void GlwPse()
    {
        
        if (CompareTag("unitSLot"))
        {
            
            foreach (Renderer renderer in glowMaterialDictionary.Keys)
            {
                foreach (Material item in glowMaterialDictionary[renderer])
                {
                    renderer.materials = glowMaterialDictionary[renderer];
                    item.SetColor("_Color", poseColor);
                }
            }
        }
    }
    public void RemoveGlow()
    {
        foreach (Renderer renderer in glowMaterialDictionary.Keys)
        {
            foreach (Material item in glowMaterialDictionary[renderer])
            {
                renderer.materials = originalMaterialDictionary[renderer];
                item.SetColor("_Color", originalGlowColor);
            }
        }
    }

    public void ToggleGlowAtk()
    {
        if (isGlowing == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                foreach (Material item in glowMaterialDictionary[renderer])
                {
                   renderer.materials = glowMaterialDictionary[renderer];
                    item.SetColor("_Color",  atkColor);
                }
            }

        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                foreach(Material item in glowMaterialDictionary[renderer])
                {
                    renderer.materials = originalMaterialDictionary[renderer];
                    item.SetColor("_Color", originalGlowColor);
                }
            }
        }
        isGlowing = !isGlowing;
    }
    public void ToggleGlowAtk(Unit selectedUnit)
    {
        if (isGlowing == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                foreach (Material item in glowMaterialDictionary[renderer])
                {

                    if((selectedUnit.GetComponent<Chara>().Classe1==Chara.Classe.Archer|| selectedUnit.GetComponent<Chara>().Classe1 == Chara.Classe.Kappa) && GetComponent<Hex>().hexType == Hex.HexType.Forest)
                    {
                        continue;
                    }
                    renderer.materials = glowMaterialDictionary[renderer];
                    item.SetColor("_Color", atkColor);
                }
            }

        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                foreach (Material item in glowMaterialDictionary[renderer])
                {
                    renderer.materials = originalMaterialDictionary[renderer];
                    item.SetColor("_Color", originalGlowColor);
                }
            }
        }
        isGlowing = !isGlowing;
    }
    public void ToggleGlowAtk(bool state)
    {
        if (isGlowing == state)
            return;
        isGlowing = !state;
        ToggleGlowAtk();
    }
    public void ToggleGlowAtk(bool state,Unit unit)
    {
        if (isGlowing == state)
            return;
        isGlowing = !state;
        ToggleGlowAtk(unit);
    }
}
