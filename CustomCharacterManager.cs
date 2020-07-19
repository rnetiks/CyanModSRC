using System;
using System.Runtime.InteropServices;
using UnityEngine;
using CyanMod;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CustomCharacterManager : MonoBehaviour
{
    private int capeId;
    private int[] capeOption;
    public GameObject character;
    private int costumeId = 1;
    private HeroCostume[] costumeOption;
    public HeroCostume currentCostume;
    private string currentSlot = "Set 1";
    private int divisionId;
    private DIVISION[] divisionOption;
    private int eyeId;
    private int[] eyeOption;
    private int faceId;
    private int[] faceOption;
    private int glassId;
    private int[] glassOption;
    public GameObject hairB;
    public GameObject hairG; 
    private int hairId;
    private int[] hairOption;
    public GameObject hairR;
    public GameObject labelACL;
    public GameObject labelBLA;
    public GameObject labelCape;
    public GameObject labelCostume;
    public GameObject labelDivision;
    public GameObject labelEye;
    public GameObject labelFace;
    public GameObject labelGAS;
    public GameObject labelGlass;
    public GameObject labelHair;
    public GameObject labelPOINT;
    public GameObject labelPreset;
    public GameObject labelSex;
    public GameObject labelSKILL;
    public GameObject labelSkin;
    public GameObject labelSPD;
    private int presetId;
    private HERO_SETUP setup;
    private int sexId;
    private SEX[] sexOption;
    private int skillId;
    private string[] skillOption;
    private int skinId;
    private int[] skinOption;
    bool showUI = true;

    private int calTotalPoints()
    {
        if (this.setup.myCostume != null)
        {
            int num = 0;
            num += this.setup.myCostume.stat.SPD;
            num += this.setup.myCostume.stat.GAS;
            num += this.setup.myCostume.stat.BLA;
            return (num + this.setup.myCostume.stat.ACL);
        }
        return 400;
    }

    private void copyBodyCostume(HeroCostume from, HeroCostume to)
    {
        to.arm_l_mesh = from.arm_l_mesh;
        to.arm_r_mesh = from.arm_r_mesh;
        to.body_mesh = from.body_mesh;
        to.body_texture = from.body_texture;
        to.uniform_type = from.uniform_type;
        to.part_chest_1_object_mesh = from.part_chest_1_object_mesh;
        to.part_chest_1_object_texture = from.part_chest_1_object_texture;
        to.part_chest_object_mesh = from.part_chest_object_mesh;
        to.part_chest_object_texture = from.part_chest_object_texture;
        to.part_chest_skinned_cloth_mesh = from.part_chest_skinned_cloth_mesh;
        to.part_chest_skinned_cloth_texture = from.part_chest_skinned_cloth_texture;
        to.division = from.division;
        to.id = from.id;
        to.costumeId = from.costumeId;
    }

    private void copyCostume(HeroCostume from, HeroCostume to, bool init = false)
    {
        this.copyBodyCostume(from, to);
        to.sex = from.sex;
        to.hair_mesh = from.hair_mesh;
        to.hair_1_mesh = from.hair_1_mesh;
        to.hair_color = new Color(from.hair_color.r, from.hair_color.g, from.hair_color.b);
        to.hairInfo = from.hairInfo;
        to.cape = from.cape;
        to.cape_mesh = from.cape_mesh;
        to.cape_texture = from.cape_texture;
        to.brand1_mesh = from.brand1_mesh;
        to.brand2_mesh = from.brand2_mesh;
        to.brand3_mesh = from.brand3_mesh;
        to.brand4_mesh = from.brand4_mesh;
        to.brand_texture = from.brand_texture;
        to._3dmg_texture = from._3dmg_texture;
        to.face_texture = from.face_texture;
        to.eye_mesh = from.eye_mesh;
        to.glass_mesh = from.glass_mesh;
        to.beard_mesh = from.beard_mesh;
        to.eye_texture_id = from.eye_texture_id;
        to.beard_texture_id = from.beard_texture_id;
        to.glass_texture_id = from.glass_texture_id;
        to.skin_color = from.skin_color;
        to.skin_texture = from.skin_texture;
        to.beard_texture_id = from.beard_texture_id;
        to.hand_l_mesh = from.hand_l_mesh;
        to.hand_r_mesh = from.hand_r_mesh;
        to.mesh_3dmg = from.mesh_3dmg;
        to.mesh_3dmg_gas_l = from.mesh_3dmg_gas_l;
        to.mesh_3dmg_gas_r = from.mesh_3dmg_gas_r;
        to.mesh_3dmg_belt = from.mesh_3dmg_belt;
        to.weapon_l_mesh = from.weapon_l_mesh;
        to.weapon_r_mesh = from.weapon_r_mesh;
        if (init)
        {
            to.stat = new HeroStat();
            to.stat.ACL = 100;
            to.stat.SPD = 100;
            to.stat.GAS = 100;
            to.stat.BLA = 100;
            to.stat.skillId = "mikasa";
        }
        else
        {
            to.stat = new HeroStat();
            to.stat.ACL = from.stat.ACL;
            to.stat.SPD = from.stat.SPD;
            to.stat.GAS = from.stat.GAS;
            to.stat.BLA = from.stat.BLA;
            to.stat.skillId = from.stat.skillId;
        }
    }

    private void CostumeDataToMyID()
    {
        int index = 0;
        for (index = 0; index < this.sexOption.Length; index++)
        {
            if (this.sexOption[index] == this.setup.myCostume.sex)
            {
                this.sexId = index;
                break;
            }
        }
        index = 0;
        while (index < this.eyeOption.Length)
        {
            if (this.eyeOption[index] == this.setup.myCostume.eye_texture_id)
            {
                this.eyeId = index;
                break;
            }
            index++;
        }
        this.faceId = -1;
        for (index = 0; index < this.faceOption.Length; index++)
        {
            if (this.faceOption[index] == this.setup.myCostume.beard_texture_id)
            {
                this.faceId = index;
                break;
            }
        }
        this.glassId = -1;
        for (index = 0; index < this.glassOption.Length; index++)
        {
            if (this.glassOption[index] == this.setup.myCostume.glass_texture_id)
            {
                this.glassId = index;
                break;
            }
        }
        for (index = 0; index < this.hairOption.Length; index++)
        {
            if (this.hairOption[index] == this.setup.myCostume.hairInfo.id)
            {
                this.hairId = index;
                break;
            }
        }
        for (index = 0; index < this.skinOption.Length; index++)
        {
            if (this.skinOption[index] == this.setup.myCostume.skin_color)
            {
                this.skinId = index;
                break;
            }
        }
        if (this.setup.myCostume.cape)
        {
            this.capeId = 1;
        }
        else
        {
            this.capeId = 0;
        }
        index = 0;
        while (index < this.divisionOption.Length)
        {
            if (this.divisionOption[index] == this.setup.myCostume.division)
            {
                this.divisionId = index;
                break;
            }
            index++;
        }
        this.costumeId = this.setup.myCostume.costumeId;
        float r = this.setup.myCostume.hair_color.r;
        float g = this.setup.myCostume.hair_color.g;
        float b = this.setup.myCostume.hair_color.b;
        this.hairR.GetComponent<UISlider>().sliderValue = r;
        this.hairG.GetComponent<UISlider>().sliderValue = g;
        this.hairB.GetComponent<UISlider>().sliderValue = b;
        for (index = 0; index < this.skillOption.Length; index++)
        {
            if (this.skillOption[index] == this.setup.myCostume.stat.skillId)
            {
                this.skillId = index;
                return;
            }
        }
    }

    private void freshLabel()
    {
        this.labelSex.GetComponent<UILabel>().text = this.sexOption[this.sexId].ToString();

        this.labelEye.GetComponent<UILabel>().text = "eye_" + this.eyeId.ToString();
        this.labelFace.GetComponent<UILabel>().text = "face_" + this.faceId.ToString();
        this.labelGlass.GetComponent<UILabel>().text = "glass_" + this.glassId.ToString();
        this.labelHair.GetComponent<UILabel>().text = "hair_" + this.hairId.ToString();
        this.labelSkin.GetComponent<UILabel>().text = "skin_" + this.skinId.ToString();
        this.labelCostume.GetComponent<UILabel>().text = "costume_" + this.costumeId.ToString();
        this.labelCape.GetComponent<UILabel>().text = "cape_" + this.capeId.ToString();
        this.labelDivision.GetComponent<UILabel>().text = this.divisionOption[this.divisionId].ToString();
        this.labelPOINT.GetComponent<UILabel>().text = "Points: " + ((400 - this.calTotalPoints())).ToString();
        this.labelSPD.GetComponent<UILabel>().text = "SPD " + this.setup.myCostume.stat.SPD.ToString();
        this.labelGAS.GetComponent<UILabel>().text = "GAS " + this.setup.myCostume.stat.GAS.ToString();
        this.labelBLA.GetComponent<UILabel>().text = "BLA " + this.setup.myCostume.stat.BLA.ToString();
        this.labelACL.GetComponent<UILabel>().text = "ACL " + this.setup.myCostume.stat.ACL.ToString();
        this.labelSKILL.GetComponent<UILabel>().text = "SKILL " + this.setup.myCostume.stat.skillId.ToString();
      
    }

    public void LoadData()
    {
        HeroCostume from = CostumeConeveter.LocalDataToHeroCostume(this.currentSlot);
        if (from != null)
        {
            this.copyCostume(from, this.setup.myCostume, false);
            this.setup.deleteCharacterComponent2();
            this.setup.setCharacterComponent();
        }
        this.CostumeDataToMyID();
        this.freshLabel();
    }

    public void nextOption(CreatePart part)
    {
        if (part == CreatePart.Preset)
        {
            this.presetId = this.toNext(this.presetId, HeroCostume.costume.Length, 0);
            this.copyCostume(HeroCostume.costume[this.presetId], this.setup.myCostume, true);
            this.CostumeDataToMyID();
            this.setup.deleteCharacterComponent2();
            this.setup.setCharacterComponent();
            this.labelPreset.GetComponent<UILabel>().text = HeroCostume.costume[this.presetId].name;
            this.freshLabel();
        }
        else
        {
            this.toOption2(part, true);
        }
    }

    public void nextStatOption(CreateStat type)
    {
        if (type == CreateStat.Skill)
        {
            this.skillId = this.toNext(this.skillId, this.skillOption.Length, 0);
            this.setup.myCostume.stat.skillId = this.skillOption[this.skillId];
            this.character.GetComponent<CharacterCreateAnimationControl>().playAttack(this.setup.myCostume.stat.skillId);
            this.freshLabel();
        }
        else if (this.calTotalPoints() < 400)
        {
            this.setStatPoint(type, 1);
        }
    }

    public void OnHairBChange(float value)
    {
        if (((this.setup != null) && (this.setup.myCostume != null)) && (this.setup.part_hair != null))
        {
            this.setup.myCostume.hair_color = new Color(this.setup.part_hair.renderer.material.color.r, this.setup.part_hair.renderer.material.color.g, value);
            this.setHairColor();
        }
    }

    public void OnHairGChange(float value)
    {
        if ((this.setup.myCostume != null) && (this.setup.part_hair != null))
        {
            this.setup.myCostume.hair_color = new Color(this.setup.part_hair.renderer.material.color.r, value, this.setup.part_hair.renderer.material.color.b);
            this.setHairColor();
        }
    }

    public void OnHairRChange(float value)
    {
        if ((this.setup.myCostume != null) && (this.setup.part_hair != null))
        {
            this.setup.myCostume.hair_color = new Color(value, this.setup.part_hair.renderer.material.color.g, this.setup.part_hair.renderer.material.color.b);
            this.setHairColor();
        }
    }
    void ChageColor(Color color)
    {
        OnHairBChange(color.b);
        OnHairRChange(color.r);
        OnHairGChange(color.g);
    }
    public void OnSoltChange(string id)
    {
        this.currentSlot = id;
    }

    public void prevOption(CreatePart part)
    {
        if (part == CreatePart.Preset)
        {
            this.presetId = this.toPrev(this.presetId, HeroCostume.costume.Length, 0);
            this.copyCostume(HeroCostume.costume[this.presetId], this.setup.myCostume, true);
            this.CostumeDataToMyID();
            this.setup.deleteCharacterComponent2();
            this.setup.setCharacterComponent();
            this.labelPreset.GetComponent<UILabel>().text = HeroCostume.costume[this.presetId].name;
            this.freshLabel();
        }
        else
        {
            this.toOption2(part, false);
        }
    }

    public void prevStatOption(CreateStat type)
    {
        if (type == CreateStat.Skill)
        {
            this.skillId = this.toPrev(this.skillId, this.skillOption.Length, 0);
            this.setup.myCostume.stat.skillId = this.skillOption[this.skillId];
            this.character.GetComponent<CharacterCreateAnimationControl>().playAttack(this.setup.myCostume.stat.skillId);
            this.freshLabel();
        }
        else
        {
            this.setStatPoint(type, -1);
        }
    }

    public void SaveData()
    {
        CostumeConeveter.HeroCostumeToLocalData(this.setup.myCostume, this.currentSlot);
    }

    private void setHairColor()
    {
        if (this.setup.part_hair != null)
        {
            this.setup.part_hair.renderer.material.color = this.setup.myCostume.hair_color;
        }
        if (this.setup.part_hair_1 != null)
        {
            this.setup.part_hair_1.renderer.material.color = this.setup.myCostume.hair_color;
        }
    }

    private void setStatPoint(CreateStat type, int pt)
    {
        switch (type)
        {
            case CreateStat.SPD:
                this.setup.myCostume.stat.SPD += pt;
                break;

            case CreateStat.GAS:
                this.setup.myCostume.stat.GAS += pt;
                break;

            case CreateStat.BLA:
                this.setup.myCostume.stat.BLA += pt;
                break;

            case CreateStat.ACL:
                this.setup.myCostume.stat.ACL += pt;
                break;
        }
        this.setup.myCostume.stat.SPD = Mathf.Clamp(this.setup.myCostume.stat.SPD, 0x4b, 0x7d);
        this.setup.myCostume.stat.GAS = Mathf.Clamp(this.setup.myCostume.stat.GAS, 0x4b, 0x7d);
        this.setup.myCostume.stat.BLA = Mathf.Clamp(this.setup.myCostume.stat.BLA, 0x4b, 0x7d);
        this.setup.myCostume.stat.ACL = Mathf.Clamp(this.setup.myCostume.stat.ACL, 0x4b, 0x7d);
        this.freshLabel();
    }

    private void Start()
    {
        PanelInformer.instance.Add("Для скрытия UI нажмите 9.", PanelInformer.LOG_TYPE.INFORMAION);
        myCyanSkin = new CyanSkin();
        myCyanSkin.name = "Settings";
        int num;
        QualitySettings.SetQualityLevel(5, true);
        this.costumeOption = HeroCostume.costumeOption;
        this.setup = this.character.GetComponent<HERO_SETUP>();
        this.setup.init();
        this.setup.myCostume = new HeroCostume();
        this.copyCostume(HeroCostume.costume[2], this.setup.myCostume, false);
        this.setup.myCostume.setMesh2();
        this.setup.setCharacterComponent();
        SEX[] sexArray = new SEX[2];
        sexArray[1] = SEX.FEMALE;
        this.sexOption = sexArray;
        this.eyeOption = new int[0x1c];
        for (num = 0; num < 0x1c; num++)
        {
            this.eyeOption[num] = num;
        }
        this.faceOption = new int[14];
        for (num = 0; num < 14; num++)
        {
            this.faceOption[num] = num + 0x20;
        }
        this.glassOption = new int[10];
        for (num = 0; num < 10; num++)
        {
            this.glassOption[num] = num + 0x30;
        }
        this.hairOption = new int[11];
        for (num = 0; num < 11; num++)
        {
            this.hairOption[num] = num;
        }
        this.skinOption = new int[3];
        for (num = 0; num < 3; num++)
        {
            this.skinOption[num] = num + 1;
        }
        this.capeOption = new int[2];
        for (num = 0; num < 2; num++)
        {
            this.capeOption[num] = num;
        }
        DIVISION[] divisionArray = new DIVISION[4];
        divisionArray[1] = DIVISION.TheGarrison;
        divisionArray[2] = DIVISION.TheMilitaryPolice;
        divisionArray[3] = DIVISION.TheSurveryCorps;
        this.divisionOption = divisionArray;
        this.skillOption = new string[] { "mikasa", "levi", "sasha", "jean", "marco", "armin", "petra" };
        this.CostumeDataToMyID();
        this.freshLabel();
        ToSS();
        LoadData();
        PanelInformer.instance.Add("Для скрытия UI нажмите 9.", PanelInformer.LOG_TYPE.INFORMAION);
        loadSetsCamera();
    }
    IEnumerator SetSkybox()
    {
       
            yield return new WaitForSeconds(2f);
            string[] dfgfdg = new string[] { "Up.jpg", "Down.jpg", "Left.jpg", "Right.jpg", "Front.jpg", "Back.jpg" };
            string[] dfgfdg2 = new string[] { "_UpTex", "_DownTex", "_LeftTex", "_RightTex", "_FrontTex", "_BackTex" };

            Material mat = Camera.current.GetComponent<Skybox>().material;
            for (int i = 0; i < dfgfdg.Length; i++)
            {
                FileInfo up_info = new FileInfo(Application.dataPath + "/SkyboxCharacterCustom/" + dfgfdg[i]);
                if (!up_info.Exists)
                {
                    UnityEngine.Debug.LogError("Cyan_mod|File Not Found. File:");
                }
                else
                {
                    Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    texture2D.LoadImage(up_info.ReadAllBytes());
                    texture2D.Apply();
                    mat.SetTexture(dfgfdg2[i], texture2D);
                }
            }
            Camera.current.GetComponent<Skybox>().material = mat;
        yield break;
    }
    void ToSS()
    {
        GameObject.Find("PopupListSlot").transform.localPosition = new Vector3(0, 9999, 0);
        hairR.transform.localPosition = new Vector3(0, 9999, 0);
        labelACL.transform.localPosition = new Vector3(0, 9999, 0);
        labelBLA.transform.localPosition = new Vector3(0, 9999, 0);
        labelCape.transform.localPosition = new Vector3(0, 9999, 0);
        labelCostume.transform.localPosition = new Vector3(0, 9999, 0);
        labelDivision.transform.localPosition = new Vector3(0, 9999, 0);
        labelEye.transform.localPosition = new Vector3(0, 9999, 0);
        labelFace.transform.localPosition = new Vector3(0, 9999, 0);
        labelGAS.transform.localPosition = new Vector3(0, 9999, 0);
        labelGlass.transform.localPosition = new Vector3(0, 9999, 0);
        labelHair.transform.localPosition = new Vector3(0, 9999, 0);
        labelPOINT.transform.localPosition = new Vector3(0, 9999, 0);
        labelPreset.transform.localPosition = new Vector3(0, 9999, 0);
        labelSex.transform.localPosition = new Vector3(0, 9999, 0);
        labelSKILL.transform.localPosition = new Vector3(0, 9999, 0);
        labelSkin.transform.localPosition = new Vector3(0, 9999, 0);
        labelSPD.transform.localPosition = new Vector3(0, 9999, 0);
        hairB.transform.localPosition = new Vector3(0, 9999, 0);
        hairG.transform.localPosition = new Vector3(0, 9999, 0);
    }
    private int toNext(int id, int Count, int start = 0)
    {
        id++;
        if (id >= Count)
        {
            id = start;
        }
        id = Mathf.Clamp(id, start, (start + Count) - 1);
        return id;
    }

    public void toOption(CreatePart part, bool next)
    {
        switch (part)
        {
            case CreatePart.Sex:
                this.sexId = !next ? this.toPrev(this.sexId, this.sexOption.Length, 0) : this.toNext(this.sexId, this.sexOption.Length, 0);
                if (this.sexId == 0)
                {
                    this.costumeId = 11;
                }
                else
                {
                    this.costumeId = 0;
                }
                this.copyCostume(this.costumeOption[this.costumeId], this.setup.myCostume, true);
                this.setup.myCostume.sex = this.sexOption[this.sexId];
                this.character.GetComponent<CharacterCreateAnimationControl>().toStand();
                this.CostumeDataToMyID();
                this.setup.deleteCharacterComponent2();
                this.setup.setCharacterComponent();
                goto Label_0624;

            case CreatePart.Eye:
                this.eyeId = !next ? this.toPrev(this.eyeId, this.eyeOption.Length, 0) : this.toNext(this.eyeId, this.eyeOption.Length, 0);
                this.setup.myCostume.eye_texture_id = this.eyeId;
                this.setup.setFacialTexture(this.setup.part_eye, this.eyeOption[this.eyeId]);
                goto Label_0624;

            case CreatePart.Face:
                this.faceId = !next ? this.toPrev(this.faceId, this.faceOption.Length, 0) : this.toNext(this.faceId, this.faceOption.Length, 0);
                this.setup.myCostume.beard_texture_id = this.faceOption[this.faceId];
                if (this.setup.part_face == null)
                {
                    this.setup.createFace();
                }
                this.setup.setFacialTexture(this.setup.part_face, this.faceOption[this.faceId]);
                goto Label_0624;

            case CreatePart.Glass:
                this.glassId = !next ? this.toPrev(this.glassId, this.glassOption.Length, 0) : this.toNext(this.glassId, this.glassOption.Length, 0);
                this.setup.myCostume.glass_texture_id = this.glassOption[this.glassId];
                if (this.setup.part_glass == null)
                {
                    this.setup.createGlass();
                }
                this.setup.setFacialTexture(this.setup.part_glass, this.glassOption[this.glassId]);
                goto Label_0624;

            case CreatePart.Hair:
                this.hairId = !next ? this.toPrev(this.hairId, this.hairOption.Length, 0) : this.toNext(this.hairId, this.hairOption.Length, 0);
                if (this.sexId == 0)
                {
                    this.setup.myCostume.hair_mesh = CostumeHair.hairsM[this.hairOption[this.hairId]].hair;
                    this.setup.myCostume.hair_1_mesh = CostumeHair.hairsM[this.hairOption[this.hairId]].hair_1;
                    this.setup.myCostume.hairInfo = CostumeHair.hairsM[this.hairOption[this.hairId]];
                    break;
                }
                this.setup.myCostume.hair_mesh = CostumeHair.hairsF[this.hairOption[this.hairId]].hair;
                this.setup.myCostume.hair_1_mesh = CostumeHair.hairsF[this.hairOption[this.hairId]].hair_1;
                this.setup.myCostume.hairInfo = CostumeHair.hairsF[this.hairOption[this.hairId]];
                break;

            case CreatePart.Skin:
                if (this.setup.myCostume.uniform_type == UNIFORM_TYPE.CasualAHSS)
                {
                    this.skinId = 2;
                }
                else
                {
                    this.skinId = !next ? this.toPrev(this.skinId, 2, 0) : this.toNext(this.skinId, 2, 0);
                }
                this.setup.myCostume.skin_color = this.skinOption[this.skinId];
                this.setup.myCostume.setTexture();
                this.setup.setSkin();
                goto Label_0624;

            case CreatePart.Costume:
                if (this.setup.myCostume.uniform_type == UNIFORM_TYPE.CasualAHSS)
                {
                    this.costumeId = 0x19;
                }
                else if (this.sexId != 0)
                {
                    this.costumeId = !next ? this.toPrev(this.costumeId, 10, 0) : this.toNext(this.costumeId, 10, 0);
                }
                else
                {
                    this.costumeId = !next ? this.toPrev(this.costumeId, 0x18, 10) : this.toNext(this.costumeId, 0x18, 10);
                }
                this.copyBodyCostume(this.costumeOption[this.costumeId], this.setup.myCostume);
                this.setup.myCostume.setMesh2();
                this.setup.myCostume.setTexture();
                this.setup.createUpperBody2();
                this.setup.createLeftArm();
                this.setup.createRightArm();
                this.setup.createLowerBody();
                goto Label_0624;

            case CreatePart.Cape:
                this.capeId = !next ? this.toPrev(this.capeId, this.capeOption.Length, 0) : this.toNext(this.capeId, this.capeOption.Length, 0);
                this.setup.myCostume.cape = this.capeId == 1;
                this.setup.myCostume.setCape();
                this.setup.myCostume.setTexture();
                this.setup.createCape2();
                goto Label_0624;

            case CreatePart.Division:
                this.divisionId = !next ? this.toPrev(this.divisionId, this.divisionOption.Length, 0) : this.toNext(this.divisionId, this.divisionOption.Length, 0);
                this.setup.myCostume.division = this.divisionOption[this.divisionId];
                this.setup.myCostume.setTexture();
                this.setup.createUpperBody2();
                goto Label_0624;

            default:
                goto Label_0624;
        }
        this.setup.createHair2();
        this.setHairColor();
    Label_0624:
        this.freshLabel();
    }

    public void toOption2(CreatePart part, bool next)
    {
        switch (part)
        {
            case CreatePart.Sex:
                this.sexId = !next ? this.toPrev(this.sexId, this.sexOption.Length, 0) : this.toNext(this.sexId, this.sexOption.Length, 0);
                if (this.sexId != 0)
                {
                    this.costumeId = 0;
                    break;
                }
                this.costumeId = 11;
                break;

            case CreatePart.Eye:
                this.eyeId = !next ? this.toPrev(this.eyeId, this.eyeOption.Length, 0) : this.toNext(this.eyeId, this.eyeOption.Length, 0);
                this.setup.myCostume.eye_texture_id = this.eyeId;
                this.setup.setFacialTexture(this.setup.part_eye, this.eyeOption[this.eyeId]);
                goto Label_0653;

            case CreatePart.Face:
                this.faceId = !next ? this.toPrev(this.faceId, this.faceOption.Length, 0) : this.toNext(this.faceId, this.faceOption.Length, 0);
                this.setup.myCostume.beard_texture_id = this.faceOption[this.faceId];
                if (this.setup.part_face == null)
                {
                    this.setup.createFace();
                }
                this.setup.setFacialTexture(this.setup.part_face, this.faceOption[this.faceId]);
                goto Label_0653;

            case CreatePart.Glass:
                this.glassId = !next ? this.toPrev(this.glassId, this.glassOption.Length, 0) : this.toNext(this.glassId, this.glassOption.Length, 0);
                this.setup.myCostume.glass_texture_id = this.glassOption[this.glassId];
                if (this.setup.part_glass == null)
                {
                    this.setup.createGlass();
                }
                this.setup.setFacialTexture(this.setup.part_glass, this.glassOption[this.glassId]);
                goto Label_0653;

            case CreatePart.Hair:
                this.hairId = !next ? this.toPrev(this.hairId, this.hairOption.Length, 0) : this.toNext(this.hairId, this.hairOption.Length, 0);
                if (this.sexId != 0)
                {
                    this.setup.myCostume.hair_mesh = CostumeHair.hairsF[this.hairOption[this.hairId]].hair;
                    this.setup.myCostume.hair_1_mesh = CostumeHair.hairsF[this.hairOption[this.hairId]].hair_1;
                    this.setup.myCostume.hairInfo = CostumeHair.hairsF[this.hairOption[this.hairId]];
                }
                else
                {
                    this.setup.myCostume.hair_mesh = CostumeHair.hairsM[this.hairOption[this.hairId]].hair;
                    this.setup.myCostume.hair_1_mesh = CostumeHair.hairsM[this.hairOption[this.hairId]].hair_1;
                    this.setup.myCostume.hairInfo = CostumeHair.hairsM[this.hairOption[this.hairId]];
                }
                this.setup.createHair2();
                this.setHairColor();
                goto Label_0653;

            case CreatePart.Skin:
                if (this.setup.myCostume.uniform_type != UNIFORM_TYPE.CasualAHSS)
                {
                    this.skinId = !next ? this.toPrev(this.skinId, 2, 0) : this.toNext(this.skinId, 2, 0);
                }
                else
                {
                    this.skinId = 2;
                }
                this.setup.myCostume.skin_color = this.skinOption[this.skinId];
                this.setup.myCostume.setTexture();
                this.setup.setSkin();
                goto Label_0653;

            case CreatePart.Costume:
                if (this.setup.myCostume.uniform_type != UNIFORM_TYPE.CasualAHSS)
                {
                    if (this.sexId != 0)
                    {
                        this.costumeId = !next ? this.toPrev(this.costumeId, 10, 0) : this.toNext(this.costumeId, 10, 0);
                    }
                    else
                    {
                        this.costumeId = !next ? this.toPrev(this.costumeId, 0x18, 10) : this.toNext(this.costumeId, 0x18, 10);
                    }
                }
                else if (this.setup.myCostume.sex != SEX.FEMALE)
                {
                    if (this.setup.myCostume.sex == SEX.MALE)
                    {
                        this.costumeId = 0x19;
                    }
                }
                else
                {
                    this.costumeId = 0x1a;
                }
                this.copyBodyCostume(this.costumeOption[this.costumeId], this.setup.myCostume);
                this.setup.myCostume.setMesh2();
                this.setup.myCostume.setTexture();
                this.setup.createUpperBody2();
                this.setup.createLeftArm();
                this.setup.createRightArm();
                this.setup.createLowerBody();
                goto Label_0653;

            case CreatePart.Cape:
                this.capeId = !next ? this.toPrev(this.capeId, this.capeOption.Length, 0) : this.toNext(this.capeId, this.capeOption.Length, 0);
                this.setup.myCostume.cape = this.capeId == 1;
                this.setup.myCostume.setCape();
                this.setup.myCostume.setTexture();
                this.setup.createCape2();
                goto Label_0653;

            case CreatePart.Division:
                this.divisionId = !next ? this.toPrev(this.divisionId, this.divisionOption.Length, 0) : this.toNext(this.divisionId, this.divisionOption.Length, 0);
                this.setup.myCostume.division = this.divisionOption[this.divisionId];
                this.setup.myCostume.setTexture();
                this.setup.createUpperBody2();
                goto Label_0653;

            default:
                goto Label_0653;
        }
        this.copyCostume(this.costumeOption[this.costumeId], this.setup.myCostume, true);
        this.setup.myCostume.sex = this.sexOption[this.sexId];
        this.character.GetComponent<CharacterCreateAnimationControl>().toStand();
        this.CostumeDataToMyID();
        this.setup.deleteCharacterComponent2();
        this.setup.setCharacterComponent();
    Label_0653:
        this.freshLabel();
    }

    private int toPrev(int id, int Count, int start = 0)
    {
        id--;
        if (id < start)
        {
            id = Count - 1;
        }
        id = Mathf.Clamp(id, start, (start + Count) - 1);
        return id;
    }

    bool is_chage_skin = false;
    int item = 0;
    string[] strings = new string[] {"Set 1","Set 2", "Set 3" };
    bool isShow = true;
    Vector2 scroolPos;
    void Settings(string names, CreatePart part)
    {
      
        GUILayout.BeginHorizontal();
        GUILayout.Label(names,GUILayout.Width(100));
        if (GUILayout.Button("<", GUILayout.Width(30)))
        {
            prevOption(part);
        }
    
        string str = "";
        if(part == CreatePart.Cape)
        {
            str = "cape_" + this.capeId.ToString();
        }
        else if(part == CreatePart.Costume)
        {
              str = "costume_" + this.costumeId.ToString();
        }
        else if (part == CreatePart.Division)
        {
            str = this.divisionOption[this.divisionId].ToString();
        }
        else if (part == CreatePart.Eye)
        {
            str = "eye_" + this.eyeId.ToString();
        }
        else if (part == CreatePart.Face)
        {
            str = "face_" + this.faceId.ToString();
        }
        else if (part == CreatePart.Glass)
        {
            str = "glass_" + this.glassId.ToString();
        }
        else if (part == CreatePart.Hair)
        {
            str = "hair_" + this.hairId.ToString();
        }
        else if (part == CreatePart.Sex)
        {
            str = this.sexOption[this.sexId].ToString();
        }
        else if (part == CreatePart.Skin)
        {
            str = "skin_" + this.skinId.ToString();
        }
        else if (part == CreatePart.Preset)
        {
            str = HeroCostume.costume[this.presetId].name;
        }
        GUILayout.Label(str);
        if (GUILayout.Button(">", GUILayout.Width(30)))
        {
            nextOption(part);
        }
        
        GUILayout.EndHorizontal();
    }
    List<SavePosition> listSavePos;
    readonly string pathSaveSetCamera = Application.dataPath + "/savesCamera.cfg";
    void loadSetsCamera()
    {
        listSavePos = new List<SavePosition>();
        FileInfo info = new FileInfo(pathSaveSetCamera);
        if (info.Exists)
        {
            string[] leans = File.ReadAllLines(pathSaveSetCamera);
            if (leans.Length > 0)
            {
                SavePosition savepos = null;
                foreach (string str in leans)
                {
                    if (str.Contains(":"))
                    {
                        string[] sre = str.Trim().Split(new char[]{':'});
                        string key = sre[0];
                        string value = sre[1];

                        if ((key = key.Trim()) != "" && (value = value.Trim()) != "")
                        {
                            if (key == "name")
                            {
                                listSavePos.Add(savepos = new SavePosition());
                                savepos.name_set = value;
                            }
                            else if (key == "x")
                            {
                                savepos.position.x = Convert.ToSingle(value);
                            }
                            else if (key == "y")
                            {
                                savepos.position.y = Convert.ToSingle(value);
                            }
                            else if (key == "z")
                            {
                                savepos.position.z = Convert.ToSingle(value);
                            }
                            else if (key == "length")
                            {
                                savepos.leng = Convert.ToSingle(value);
                            }
                        }
                    }
                }
            }
        }
    }
    void SaveSetCamera()
    {
        string str = "";
        foreach(var ss in  listSavePos)
        {
            str = str + "name:" + ss.name_set.Trim() + "\n";
            str = str + "length:" + ss.leng + "\n";
            str = str + "x:" + ss.position.x + "\n";
            str = str + "y:" + ss.position.y + "\n";
            str = str + "z:" + ss.position.z + "\n";
        }
        File.WriteAllText(pathSaveSetCamera, str, System.Text.Encoding.UTF8);
      
        
    }
    Vector2 scrolPos;
    bool isDelll = false;
    string text43 = "CameraPos_" + UnityEngine.Random.Range(9999,999999);
    void GUI2()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 200, 400f));
        {
            GUILayout.Label("Позиции Камеры:");
            text43 = GUILayout.TextField(text43);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.Width(100f)))
            {
                if ((text43 = text43.Trim().Replace(":","")) != "")
                {
                    listSavePos.Add(new SavePosition(text43, BTN_rotate_character.instance.camera.transform.position, BTN_rotate_character.instance.distance));
                }
            }
            isDelll = GUILayout.Toggle(isDelll, "Удаление.");
                 GUILayout.EndHorizontal();
            scrolPos = GUILayout.BeginScrollView(scrolPos);
            if (listSavePos != null && listSavePos.Count > 0)
            {
                foreach (var ss in listSavePos)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(ss.name_set + "/" + "x" + ss.position.x.ToString("F") + "y" + ss.position.y.ToString("F") + "z" + ss.position.z.ToString("F")))
                    {
                        BTN_rotate_character.instance.camera.transform.position = ss.position;
                        BTN_rotate_character.instance.distance = ss.leng;
                        return;
                    }
                    if (isDelll && GUILayout.Button("x", GUILayout.Width(30f)))
                    {
                        listSavePos.Remove(ss);
                        return;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Сохранить"))
            {
                SaveSetCamera();
                PanelInformer.instance.Add("Сохранено.", PanelInformer.LOG_TYPE.INFORMAION);
            }
            if (GUILayout.Button("Загрузить"))
            {
                loadSetsCamera();
                PanelInformer.instance.Add("Загружено.", PanelInformer.LOG_TYPE.INFORMAION);

            }
                GUILayout.EndHorizontal();
       
        }
        GUILayout.EndArea();
    }
    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            showUI = !showUI;
        }
        if (showUI)
        {
            GUI.backgroundColor = INC.gui_color;
            if (isShow)
            {
                GUI2();
                GUILayout.BeginArea(new Rect(0, 0, 280, Screen.height));
                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<<<", GUILayout.Width(80f)))
                {
                    isShow = false;
                }
                GUILayout.Label(INC.la("character_set"));
                GUILayout.EndHorizontal();
                int i = item;
                item = GUILayout.SelectionGrid(item, strings, 3);
                if (item != i)
                {
                    currentSlot = strings[item];
                    LoadData();
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("save")))
                {
                    SaveData();
                }
                if (GUILayout.Button(INC.la("load")))
                {
                    LoadData();
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button(!is_chage_skin ? INC.la("set_skin_charecter") : INC.la("back_charecter")))
                {
                    is_chage_skin = !is_chage_skin;
                }

                if (is_chage_skin)
                {
                    SkinGUI();
                }
                else
                {
                    Settings(INC.la("present_character"), CreatePart.Preset);
                    scroolPos = GUILayout.BeginScrollView(scroolPos);
                    Settings(INC.la("sex_charecter"), CreatePart.Sex);
                    Settings(INC.la("eyes_charecter"), CreatePart.Eye);
                    Settings(INC.la("face_charecter"), CreatePart.Face);
                    Settings(INC.la("glass_charecter"), CreatePart.Glass);
                    Settings(INC.la("hair_charecter"), CreatePart.Hair);

                    if (this.setup.part_hair != null)
                    {
                        this.setup.part_hair.renderer.material.color = cext.color_toGUI(this.setup.part_hair.renderer.material.color, INC.la("color_hair_character"), false);
                        if (this.setup.part_hair.renderer.material.color != this.setup.myCostume.hair_color)
                        {
                            ChageColor(this.setup.part_hair.renderer.material.color);
                        }
                    }
                    Settings(INC.la("skin_charecter"), CreatePart.Skin);
                    Settings(INC.la("costume_charecter"), CreatePart.Costume);
                    Settings(INC.la("division_charecter"), CreatePart.Cape);
                    Settings(INC.la("cape_charecter"), CreatePart.Division);

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(INC.la("skill_charecter"), GUILayout.Width(100));
                    if (GUILayout.Button("<", GUILayout.Width(30)))
                    {
                        prevStatOption(CreateStat.Skill);
                    }
                    GUILayout.Label(this.skillOption[this.skillId]);
                    if (GUILayout.Button(">", GUILayout.Width(30)))
                    {
                        nextStatOption(CreateStat.Skill);
                    }


                    GUILayout.EndHorizontal();
                    GUILayout.Label(INC.la("stats_charecter"));
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("<", GUILayout.Width(30)))
                    {
                        prevStatOption(CreateStat.SPD);
                    }
                    GUILayout.Label("SPD:" + this.setup.myCostume.stat.SPD);
                    if (GUILayout.Button(">", GUILayout.Width(30)))
                    {
                        nextStatOption(CreateStat.SPD);
                    }
                    if (GUILayout.Button("<", GUILayout.Width(30)))
                    {
                        prevStatOption(CreateStat.BLA);
                    }
                    GUILayout.Label("BLA:" + this.setup.myCostume.stat.BLA);
                    if (GUILayout.Button(">", GUILayout.Width(30)))
                    {
                        nextStatOption(CreateStat.BLA);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("<", GUILayout.Width(30)))
                    {
                        prevStatOption(CreateStat.ACL);
                    }
                    GUILayout.Label("ACL:" + this.setup.myCostume.stat.ACL);
                    if (GUILayout.Button(">", GUILayout.Width(30)))
                    {
                        nextStatOption(CreateStat.ACL);
                    }
                    if (GUILayout.Button("<", GUILayout.Width(30)))
                    {
                        prevStatOption(CreateStat.GAS);
                    }
                    GUILayout.Label("GAS:" + this.setup.myCostume.stat.GAS);
                    if (GUILayout.Button(">", GUILayout.Width(30)))
                    {
                        nextStatOption(CreateStat.GAS);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Label(INC.la("points_character") + ((400 - this.calTotalPoints())).ToString());
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();
                if (GUILayout.Button(INC.la("back_fromcc")))
                {
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                    FengGameManagerMKII.instance.gameStart = false;
                    FengGameManagerMKII.instance.MenuOn = false;
                    UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("MultiplayerManager"));
                    Application.LoadLevel("menu");
                }
                GUILayout.EndArea();
            }
            else
            {
                if (GUILayout.Button(">>>", GUILayout.Width(80f)))
                {
                    isShow = true;
                }
            }
        }
    }
    CyanSkin myCyanSkin;
    bool is_preset = false;
    Vector2 scrools_pos2;
    string log = "";
    void SkinGUI()
    {
        if (GUILayout.Button(INC.la("set_preset_characters")))
        {
            is_preset = !is_preset;
        }
        scrools_pos2 = GUILayout.BeginScrollView(scrools_pos2);
        if (!is_preset)
        {
            GUILayout.Label(INC.la("s_hair_model"));
            myCyanSkin.hair = GUILayout.TextField(myCyanSkin.hair);
            GUILayout.Label(INC.la("s_eyes"));
            myCyanSkin.eyes = GUILayout.TextField(myCyanSkin.eyes);
            GUILayout.Label(INC.la("s_glass"));
            myCyanSkin.glass = GUILayout.TextField(myCyanSkin.glass);
            GUILayout.Label(INC.la("s_face"));
            myCyanSkin.face = GUILayout.TextField(myCyanSkin.face);
            GUILayout.Label(INC.la("s_skin"));
            myCyanSkin.skin = GUILayout.TextField(myCyanSkin.skin);
            GUILayout.Label(INC.la("s_hoodie_model"));
            myCyanSkin.hoodie = GUILayout.TextField(myCyanSkin.hoodie);
            GUILayout.Label(INC.la("s_costume_model"));
            myCyanSkin.costume = GUILayout.TextField(myCyanSkin.costume);
            GUILayout.Label(INC.la("s_logo_and_cape"));
            myCyanSkin.logo_and_cape = GUILayout.TextField(myCyanSkin.logo_and_cape);
            GUILayout.Label(INC.la("s_3dmg_model_right_gun"));
            myCyanSkin.dmg_right = GUILayout.TextField(myCyanSkin.dmg_right);
            GUILayout.Label(INC.la("s_3dmg_model_left_gun"));
            myCyanSkin.dmg_left = GUILayout.TextField(myCyanSkin.dmg_left);

            if (GUILayout.Button(INC.la("set_skinned_characters")))
            {
                FengGameManagerMKII.linkHash[0].Clear();
                FengGameManagerMKII.linkHash[1].Clear();
                FengGameManagerMKII.linkHash[2].Clear();
                CyanSkin skin = myCyanSkin;
                string text14 = skin.horse + "," + skin.hair + "," + skin.eyes + "," + skin.glass + "," + skin.face + "," + skin.skin + "," + skin.costume + "," + skin.logo_and_cape + "," + skin.dmg_right + "," + skin.dmg_left + "," + skin.gas + "," + skin.hoodie + "," + skin.weapon_trail;
                base.StartCoroutine(loadskinE(text14));
            }
            GUILayout.Label(log);
        }
        else
        {
            foreach (CyanSkin skin in INC.cSkins)
            {
                if (GUILayout.Button(skin.name))
                {
                    myCyanSkin = skin;
                    is_preset = !is_preset;
                }
            }
        }
        GUILayout.EndScrollView();
    }
    GameObject hero;
    GameObject findHero
    {
        get
        {
            if (hero == null)
            {
              GameObject[] gm =  FindObjectsOfType<GameObject>();
              foreach (GameObject gms in gm)
              {
                  if (gms != null && gms.name.StartsWith("AOTTG_HERO 1"))
                  {
                      return hero = gms;
                  }
              }
            }
            return hero;
        }
    }
    public IEnumerator loadskinE( string url)
    {
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int)FengGameManagerMKII.settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        string[] iteratorVariable2 = url.Split(new char[] { ',' });
        bool iteratorVariable3 = false;
        if (((int)FengGameManagerMKII.settings[15]) == 0)
        {
            iteratorVariable3 = true;
        }
        if (this.setup.part_hair_1 != null)
        {
            Renderer renderer = this.setup.part_hair_1.renderer;
            if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                {
                    WWW link = new WWW(iteratorVariable2[1]);
                    yield return link;
                    Texture2D iteratorVariable8 = cext.loadimage(link, mipmap, 0x30d40);
                    link.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        iteratorVariable1 = true;
                        if (this.setup.myCostume.hairInfo.id >= 0)
                        {
                            renderer.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                        }
                        renderer.material.mainTexture = iteratorVariable8;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], renderer.material);
                        renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                    else
                    {
                        renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else
                {
                    renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                }
            }
            else if (iteratorVariable2[1].ToLower() == "transparent")
            {
                renderer.enabled = false;
            }
        }
        if (this.setup.part_cape != null)
        {
            Renderer iteratorVariable9 = this.setup.part_cape.renderer;
            if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                {
                    WWW iteratorVariable10 = new WWW(iteratorVariable2[7]);
                    yield return iteratorVariable10;
                    Texture2D iteratorVariable11 = cext.loadimage(iteratorVariable10, mipmap, 0x30d40);
                    iteratorVariable10.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable9.material.mainTexture = iteratorVariable11;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable9.material);
                        iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                    else
                    {
                        iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else
                {
                    iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                }
            }
            else if (iteratorVariable2[7].ToLower() == "transparent")
            {
                iteratorVariable9.enabled = false;
            }
        }
        if (this.setup.part_chest_3 != null)
        {
            Renderer iteratorVariable12 = this.setup.part_chest_3.renderer;
            if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                {
                    WWW iteratorVariable13 = new WWW(iteratorVariable2[6]);
                    yield return iteratorVariable13;
                    Texture2D iteratorVariable14 = cext.loadimage(iteratorVariable13, mipmap, 0x7a120);
                    iteratorVariable13.Dispose();
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable12.material.mainTexture = iteratorVariable14;
                        FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable12.material);
                        iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                    else
                    {
                        iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else
                {
                    iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                }
            }
            else if (iteratorVariable2[6].ToLower() == "transparent")
            {
                iteratorVariable12.enabled = false;
            }
        }
        foreach (Renderer iteratorVariable15 in findHero.GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[1]))
            {
                if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        WWW iteratorVariable16 = new WWW(iteratorVariable2[1]);
                        yield return iteratorVariable16;
                        Texture2D iteratorVariable17 = cext.loadimage(iteratorVariable16, mipmap, 0x30d40);
                        iteratorVariable16.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                        {
                            iteratorVariable1 = true;
                            if (this.setup.myCostume.hairInfo.id >= 0)
                            {
                                iteratorVariable15.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                            }
                            iteratorVariable15.material.mainTexture = iteratorVariable17;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else if (iteratorVariable2[1].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[2]))
            {
                if ((iteratorVariable2[2].EndsWith(".jpg") || iteratorVariable2[2].EndsWith(".png")) || iteratorVariable2[2].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                    {
                        WWW iteratorVariable18 = new WWW(iteratorVariable2[2]);
                        yield return iteratorVariable18;
                        Texture2D iteratorVariable19 = cext.loadimage(iteratorVariable18, mipmap, 0x30d40);
                        iteratorVariable18.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable19;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[2], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                    }
                }
                else if (iteratorVariable2[2].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[3]))
            {
                if ((iteratorVariable2[3].EndsWith(".jpg") || iteratorVariable2[3].EndsWith(".png")) || iteratorVariable2[3].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                    {
                        WWW iteratorVariable20 = new WWW(iteratorVariable2[3]);
                        yield return iteratorVariable20;
                        Texture2D iteratorVariable21 = cext.loadimage(iteratorVariable20, mipmap, 0x30d40);
                        iteratorVariable20.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable21;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[3], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                    }
                }
                else if (iteratorVariable2[3].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[4]))
            {
                if ((iteratorVariable2[4].EndsWith(".jpg") || iteratorVariable2[4].EndsWith(".png")) || iteratorVariable2[4].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                    {
                        WWW iteratorVariable22 = new WWW(iteratorVariable2[4]);
                        yield return iteratorVariable22;
                        Texture2D iteratorVariable23 = cext.loadimage(iteratorVariable22, mipmap, 0x30d40);
                        iteratorVariable22.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable23;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[4], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                    }
                }
                else if (iteratorVariable2[4].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[5]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[6])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[10]))
            {
                if ((iteratorVariable2[5].EndsWith(".jpg") || iteratorVariable2[5].EndsWith(".png")) || iteratorVariable2[5].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                    {
                        WWW iteratorVariable24 = new WWW(iteratorVariable2[5]);
                        yield return iteratorVariable24;
                        Texture2D iteratorVariable25 = cext.loadimage(iteratorVariable24, mipmap, 0x30d40);
                        iteratorVariable24.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable25;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[5], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                    }
                }
                else if (iteratorVariable2[5].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[7]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[8])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[9]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x18])))
            {
                if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        WWW iteratorVariable26 = new WWW(iteratorVariable2[6]);
                        yield return iteratorVariable26;
                        Texture2D iteratorVariable27 = cext.loadimage(iteratorVariable26, mipmap, 0x7a120);
                        iteratorVariable26.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable27;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else if (iteratorVariable2[6].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[12]))
            {
                if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        WWW iteratorVariable28 = new WWW(iteratorVariable2[7]);
                        yield return iteratorVariable28;
                        Texture2D iteratorVariable29 = cext.loadimage(iteratorVariable28, mipmap, 0x30d40);
                        iteratorVariable28.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable29;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else if (iteratorVariable2[7].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[15]) || ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[13]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a])) && !iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[8].EndsWith(".jpg") || iteratorVariable2[8].EndsWith(".png")) || iteratorVariable2[8].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                    {
                        WWW iteratorVariable30 = new WWW(iteratorVariable2[8]);
                        yield return iteratorVariable30;
                        Texture2D iteratorVariable31 = cext.loadimage(iteratorVariable30, mipmap, 0x7a120);
                        iteratorVariable30.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable31;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[8], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                    }
                }
                else if (iteratorVariable2[8].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x10])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a]) && iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[9].EndsWith(".jpg") || iteratorVariable2[9].EndsWith(".png")) || iteratorVariable2[9].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                    {
                        WWW iteratorVariable32 = new WWW(iteratorVariable2[9]);
                        yield return iteratorVariable32;
                        Texture2D iteratorVariable33 = cext.loadimage(iteratorVariable32, mipmap, 0x7a120);
                        iteratorVariable32.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable33;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[9], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                    }
                }
                else if (iteratorVariable2[9].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name == FengGameManagerMKII.s[0x12]) && iteratorVariable3)
            {
                if ((iteratorVariable2[10].EndsWith(".jpg") || iteratorVariable2[10].EndsWith(".png")) || iteratorVariable2[10].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                    {
                        WWW iteratorVariable34 = new WWW(iteratorVariable2[10]);
                        yield return iteratorVariable34;
                        Texture2D iteratorVariable35 = cext.loadimage(iteratorVariable34, mipmap, 0x30d40);
                        iteratorVariable34.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable35;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[10], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                    }
                }
                else if (iteratorVariable2[10].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x19]))
            {
                if ((iteratorVariable2[11].EndsWith(".jpg") || iteratorVariable2[11].EndsWith(".png")) || iteratorVariable2[11].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                    {
                        WWW iteratorVariable36 = new WWW(iteratorVariable2[11]);
                        yield return iteratorVariable36;
                        Texture2D iteratorVariable37 = cext.loadimage(iteratorVariable36, mipmap, 0x30d40);
                        iteratorVariable36.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable37;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[11], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                    }
                }
                else if (iteratorVariable2[11].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
        }

        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
        yield break;
    }
    public class SavePosition
    {
        public Vector3 position;
        public float leng;
        public string name_set;
        public SavePosition(string n, Vector3 v, float s)
        {
           name_set = n; 
            position = v;
            leng = s;
        }
        public SavePosition()
        {
            name_set = "";
            position = Vector3.zero; 
            leng = 0f;
        }
    }
}

