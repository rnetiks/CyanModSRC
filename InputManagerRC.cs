using System;
using UnityEngine;

public class InputManagerRC
{
    public KeyCode[] cannonKeys;
    public int[] cannonWheel;
    public KeyCode[] horseKeys;
    public int[] horseWheel;
    public KeyCode[] humanKeys;
    public int[] humanWheel;
    public KeyCode[] levelKeys;
    public int[] levelWheel;
    public KeyCode[] titanKeys;
    public int[] titanWheel;
    public KeyCode[] cyanKeys;
    public int[] cyanWheel;
    public KeyCode[] customKeys;
    public int[] customWheel;

    public InputManagerRC()
    {
        int num;

        this.humanWheel = new int[8];
        this.humanKeys = new KeyCode[8];
        this.horseWheel = new int[7];
        this.horseKeys = new KeyCode[7];
        this.titanWheel = new int[19];
        this.titanKeys = new KeyCode[19];
        this.levelWheel = new int[0x11];
        this.levelKeys = new KeyCode[0x11];
        this.cannonWheel = new int[7];
        this.cannonKeys = new KeyCode[7];
        this.cyanWheel = new int[20];
        this.cyanKeys = new KeyCode[20];
        this.customWheel = new int[25];
        this.customKeys = new KeyCode[25];


        for (num = 0; num < this.customWheel.Length; num++)
        {
            this.customWheel[num] = 0;
            this.customKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.cyanWheel.Length; num++)
        {
            this.cyanWheel[num] = 0;
            this.cyanKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.humanWheel.Length; num++)
        {
            this.humanWheel[num] = 0;
            this.humanKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.cannonWheel.Length; num++)
        {
            this.cannonWheel[num] = 0;
            this.cannonKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.horseWheel.Length; num++)
        {
            this.horseWheel[num] = 0;
            this.horseKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.titanWheel.Length; num++)
        {
            this.titanWheel[num] = 0;
            this.titanKeys[num] = KeyCode.None;
        }
        for (num = 0; num < this.levelWheel.Length; num++)
        {
            this.levelWheel[num] = 0;
            this.levelKeys[num] = KeyCode.None;
        }
    }
    public bool isInputCustom(int code)
    {
        if (this.customWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.customWheel[code]) > 0f);
        }
        return Input.GetKey(this.customKeys[code]);
    }
    public bool isInputCannon(int code)
    {
        if (this.cannonWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.cannonWheel[code]) > 0f);
        }
        return Input.GetKey(this.cannonKeys[code]);
    }

    public bool isInputCannonDown(int code)
    {
        if (this.cannonWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.cannonWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.cannonKeys[code]);
    }

    public bool isInputHorse(int code)
    {
        if (this.horseWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.horseWheel[code]) > 0f);
        }
        return Input.GetKey(this.horseKeys[code]);
    }

    public bool isInputHorseDown(int code)
    {
        if (this.horseWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.horseWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.horseKeys[code]);
    }
    public bool isInputCustomKeyDown(int code)
    {
        if (this.customWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.customWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.customKeys[code]);
    }
    public bool isInputCustomKeyUp(int code)
    {
        if (this.customWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.customWheel[code]) > 0f);
        }
        return Input.GetKeyUp(this.customKeys[code]);
    }
    public bool isInputCustomKey(int code)
    {
        if (this.customWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.customWheel[code]) > 0f);
        }
        return Input.GetKey(this.customKeys[code]);
    }
    public bool isInputCyanKeyDown(int code)
    {
        if (this.cyanWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.cyanWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.cyanKeys[code]);
    }
    public bool isInputCyanKey(int code)
    {
        if (this.cyanWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.cyanWheel[code]) > 0f);
        }
        return Input.GetKey(this.cyanKeys[code]);
    }
    public bool isInputHuman(int code)
    {
        if (this.humanWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.humanWheel[code]) > 0f);
        }
        return Input.GetKey(this.humanKeys[code]);
    }

    public bool isInputHumanDown(int code)
    {
        if (this.humanWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.humanWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.humanKeys[code]);
    }

    public bool isInputLevel(int code)
    {
        if (this.levelWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.levelWheel[code]) > 0f);
        }
        return Input.GetKey(this.levelKeys[code]);
    }

    public bool isInputLevelDown(int code)
    {
        if (this.levelWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.levelWheel[code]) > 0f);
        }
        return Input.GetKeyDown(this.levelKeys[code]);
    }

    public bool isInputTitan(int code)
    {
        if (this.titanWheel[code] != 0)
        {
            return ((Input.GetAxis("Mouse ScrollWheel") * this.titanWheel[code]) > 0f);
        }
        return Input.GetKey(this.titanKeys[code]);
    }

    public void setInputCannon(int code, string setting)
    {
        this.cannonKeys[code] = KeyCode.None;
        this.cannonWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.cannonWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.cannonWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.cannonKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }

    public void setInputHorse(int code, string setting)
    {
        this.horseKeys[code] = KeyCode.None;
        this.horseWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.horseWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.horseWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.horseKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }
    public void setInputCyan(int code, string setting)
    {
        this.cyanKeys[code] = KeyCode.None;
        this.cyanWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.cyanWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.cyanWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.cyanKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }
    public void setInputCustom(int code, string setting)
    {
        this.customKeys[code] = KeyCode.None;
        this.customWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.customWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.customWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.customKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }
    public void setInputHuman(int code, string setting)
    {
        this.humanKeys[code] = KeyCode.None;
        this.humanWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.humanWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.humanWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.humanKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }

    public void setInputLevel(int code, string setting)
    {
        this.levelKeys[code] = KeyCode.None;
        this.levelWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.levelWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.levelWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.levelKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }

    public void setInputTitan(int code, string setting)
    {
        this.titanKeys[code] = KeyCode.None;
        this.titanWheel[code] = 0;
        if (setting == "Scroll Up")
        {
            this.titanWheel[code] = 1;
        }
        else if (setting == "Scroll Down")
        {
            this.titanWheel[code] = -1;
        }
        else if (Enum.IsDefined(typeof(KeyCode), setting))
        {
            this.titanKeys[code] = (KeyCode)Enum.Parse(typeof(KeyCode), setting);
        }
    }
}

