using System;

public class Region
{
    public CloudRegionCode Code;
    public string HostAndPort;
    public int Ping;

    public static CloudRegionCode Parse(string codeAsString)
    {
        codeAsString = codeAsString.ToLower();
        CloudRegionCode none = CloudRegionCode.none;
        if (Enum.IsDefined(typeof(CloudRegionCode), codeAsString))
        {
            none = (CloudRegionCode) ((int) Enum.Parse(typeof(CloudRegionCode), codeAsString));
        }
        return none;
    }

    public override string ToString()
    {
        return String.Format("'{0}' 	{1}ms 	{2}", this.Code, this.Ping, this.HostAndPort);
    }
}

