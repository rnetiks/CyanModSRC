using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PhotonPingManager
{
    public static int Attempts = 5;
    public static bool IgnoreInitialAttempt = true;
    public static int MaxMilliseconsPerPing = 800;
    private int PingsRunning;
    public bool UseNative;

    [DebuggerHidden]
    public IEnumerator PingSocket(Region region)
    {
        return new PingSocketc__IteratorB
        {
            region = region,
            f__this = this
        };
    }

    public static string ResolveHost(string hostName)
    {
        try
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
            if (hostAddresses.Length == 1)
            {
                return hostAddresses[0].ToString();
            }
            for (int i = 0; i < hostAddresses.Length; i++)
            {
                IPAddress address = hostAddresses[i];
                if (address != null)
                {
                    string str = address.ToString();
                    if (str.IndexOf('.') >= 0)
                    {
                        return str;
                    }
                }
            }
        }
        catch (Exception exception)
        {
            UnityEngine.Debug.Log("Exception caught! " + exception.Source + " Message: " + exception.Message);
        }
        return string.Empty;
    }

    public Region BestRegion
    {
        get
        {
            Region region = null;
            int ping = 0x7fffffff;
            foreach (Region region2 in PhotonNetwork.networkingPeer.AvailableRegions)
            {
                UnityEngine.Debug.Log("BestRegion checks region: " + region2);
                if ((region2.Ping != 0) && (region2.Ping < ping))
                {
                    ping = region2.Ping;
                    region = region2;
                }
            }
            return region;
        }
    }

    public bool Done
    {
        get { return (this.PingsRunning == 0); }
    }

    [CompilerGenerated]
    private sealed class PingSocketc__IteratorB : IEnumerator<object>, IDisposable, IEnumerator
    {
        internal string cleanIpOfRegion__3;
        internal Exception e__8;
        internal PhotonPingManager f__this;
        internal int i__5;
        internal int indexOfColon__4;
        internal bool overtime__6;
        internal PhotonPing ping__0;
        internal Region region;
        internal int replyCount__2;
        internal int rtt__9;
        internal float rttSum__1;
        internal object Scurrent;
        internal int SPC;
        internal Stopwatch sw__7;

        [DebuggerHidden]
        public void Dispose()
        {
            this.SPC = -1;
        }

        public bool MoveNext()
        {
            uint sPC = (uint) this.SPC;
            this.SPC = -1;
            switch (sPC)
            {
                case 0:
                    this.region.Ping = PhotonPingManager.Attempts * PhotonPingManager.MaxMilliseconsPerPing;
                    this.f__this.PingsRunning++;
                    if (PhotonHandler.PingImplementation == typeof(PingNativeDynamic))
                    {
                        UnityEngine.Debug.Log("Using constructor for new PingNativeDynamic()");
                        this.ping__0 = new PingNativeDynamic();
                        break;
                    }
                    this.ping__0 = (PhotonPing) Activator.CreateInstance(PhotonHandler.PingImplementation);
                    break;

                case 1:
                case 2:
                case 3:
                    this.SPC = -1;
                    goto Label_0278;

                default:
                    goto Label_0278;
            }
            this.rttSum__1 = 0f;
            this.replyCount__2 = 0;
            this.cleanIpOfRegion__3 = this.region.HostAndPort;
            this.indexOfColon__4 = this.cleanIpOfRegion__3.LastIndexOf(':');
            if (this.indexOfColon__4 > 1)
            {
                this.cleanIpOfRegion__3 = this.cleanIpOfRegion__3.Substring(0, this.indexOfColon__4);
            }
            this.cleanIpOfRegion__3 = PhotonPingManager.ResolveHost(this.cleanIpOfRegion__3);
            this.i__5 = 0;
            while (this.i__5 < PhotonPingManager.Attempts)
            {
                this.overtime__6 = false;
                this.sw__7 = new Stopwatch();
                this.sw__7.Start();
                try
                {
                    this.ping__0.StartPing(this.cleanIpOfRegion__3);
                    goto Label_01AD;
                }
                catch (Exception exception)
                {
                    this.e__8 = exception;
                    UnityEngine.Debug.Log("catched: " + this.e__8);
                    this.f__this.PingsRunning--;
                    break;
                }
            Label_0179:
                if (this.sw__7.ElapsedMilliseconds >= PhotonPingManager.MaxMilliseconsPerPing)
                {
                    this.overtime__6 = true;
                    goto Label_01BA;
                }
                this.Scurrent = 0;
                this.SPC = 1;
                goto Label_027A;
            Label_01AD:
                if (!this.ping__0.Done())
                {
                    goto Label_0179;
                }
            Label_01BA:
                this.rtt__9 = (int) this.sw__7.ElapsedMilliseconds;
                if ((!PhotonPingManager.IgnoreInitialAttempt || (this.i__5 != 0)) && (this.ping__0.Successful && !this.overtime__6))
                {
                    this.rttSum__1 += this.rtt__9;
                    this.replyCount__2++;
                    this.region.Ping = (int) (this.rttSum__1 / ((float) this.replyCount__2));
                }
                this.Scurrent = new WaitForSeconds(0.1f);
                this.SPC = 2;
                goto Label_027A;
            }
            this.f__this.PingsRunning--;
            this.Scurrent = null;
            this.SPC = 3;
            goto Label_027A;
        Label_0278:
            return false;
        Label_027A:
            return true;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            get { return this.Scurrent; }
        }

        object IEnumerator.Current
        {
            get { return this.Scurrent; }
        }
    }
}

