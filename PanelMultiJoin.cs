using System;
using System.Collections;
using UnityEngine;

public class PanelMultiJoin : MonoBehaviour
{
    public GameObject[] items;

    private int currentPage = 1;

    private int totalPage = 1;

    private float elapsedTime = 10f;

    private ArrayList filterRoom;

    public static bool Enabeled = true;

    private string filter = string.Empty;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            this.items[i].SetActive(true);
            this.items[i].GetComponentInChildren<UILabel>().text = string.Empty;
            this.items[i].SetActive(false);
        }
    }

    private void showlist()
    {
        if (this.filter == string.Empty)
        {
            if (PhotonNetwork.GetRoomList().Length > 0)
            {
                this.totalPage = (PhotonNetwork.GetRoomList().Length - 1) / 10 + 1;
            }
            else
            {
                this.totalPage = 1;
            }
        }
        else
        {
            this.updateFilterRooms();
            if (this.filterRoom.Count > 0)
            {
                this.totalPage = (this.filterRoom.Count - 1) / 10 + 1;
            }
            else
            {
                this.totalPage = 1;
            }
        }
        if (this.currentPage < 1)
        {
            this.currentPage = this.totalPage;
        }
        if (this.currentPage > this.totalPage)
        {
            this.currentPage = 1;
        }
        this.showServerList();
    }

    private string getServerDataString(RoomInfo room)
    {
        string[] array = room.name.Split(new char[]
		{
			"`"[0]
		});
        return string.Concat(new object[]
		{
			(!(array[5] == string.Empty)) ? "[PWD]" : string.Empty,
			array[0],
			"/",
			array[1],
			"/",
			array[2],
			"/",
			array[4],
			" ",
			room.playerCount,
			"/",
			room.maxPlayers
		});
    }

    private void showServerList()
    {
        if (PhotonNetwork.GetRoomList().Length != 0)
        {
            if (this.filter == string.Empty)
            {
                for (int i = 0; i < 10; i++)
                {
                    int num = 10 * (this.currentPage - 1) + i;
                    if (num < PhotonNetwork.GetRoomList().Length)
                    {
                        this.items[i].SetActive(true);
                        this.items[i].GetComponentInChildren<UILabel>().text = this.getServerDataString(PhotonNetwork.GetRoomList()[num]);
                        this.items[i].GetComponentInChildren<BTN_Connect_To_Server_On_List>().roomName = PhotonNetwork.GetRoomList()[num].name;
                    }
                    else
                    {
                        this.items[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    int num2 = 10 * (this.currentPage - 1) + i;
                    if (num2 < this.filterRoom.Count)
                    {
                        RoomInfo roomInfo = (RoomInfo)this.filterRoom[num2];
                        this.items[i].SetActive(true);
                        this.items[i].GetComponentInChildren<UILabel>().text = this.getServerDataString(roomInfo);
                        this.items[i].GetComponentInChildren<BTN_Connect_To_Server_On_List>().roomName = roomInfo.name;
                    }
                    else
                    {
                        this.items[i].SetActive(false);
                    }
                }
            }
            CyanMod.CachingsGM.Find("LabelServerListPage").GetComponent<UILabel>().text = this.currentPage + "/" + this.totalPage;
            return;
        }
        for (int j = 0; j < this.items.Length; j++)
        {
            this.items[j].SetActive(false);
        }
        CyanMod.CachingsGM.Find("LabelServerListPage").GetComponent<UILabel>().text = this.currentPage + "/" + this.totalPage;
    }

    public void pageUp()
    {
        this.currentPage--;
        if (this.currentPage < 1)
        {
            this.currentPage = this.totalPage;
        }
        this.showServerList();
    }

    public void pageDown()
    {
        this.currentPage++;
        if (this.currentPage > this.totalPage)
        {
            this.currentPage = 1;
        }
        this.showServerList();
    }

    private void OnEnable()
    {
        this.currentPage = 1;
        this.totalPage = 0;
        this.refresh();
    }

    private void OnDisable()
    {
    }

    public void refresh()
    {
        this.showlist();
    }

    public void connectToIndex(int index, string roomName)
    {
        int i;
        for (i = 0; i < 10; i++)
        {
            this.items[i].SetActive(false);
        }
        i = 10 * (this.currentPage - 1) + index;
        string[] array = roomName.Split(new char[]
		{
			"`"[0]
		});
        if (array[5] != string.Empty)
        {
            PanelMultiJoinPWD.Password = array[5];
            PanelMultiJoinPWD.roomName = roomName;
            NGUITools.SetActive(UIMainReferences.instance.PanelMultiPWD, true);
            NGUITools.SetActive(UIMainReferences.instance.panelMultiROOM, false);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    private void OnFilterSubmit(string content)
    {
        this.filter = content;
        this.updateFilterRooms();
        this.showlist();
    }

    private void updateFilterRooms()
    {
        this.filterRoom = new ArrayList();
        if (this.filter == string.Empty)
        {
            return;
        }
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        for (int i = 0; i < roomList.Length; i++)
        {
            RoomInfo roomInfo = roomList[i];
            if (roomInfo.name.ToUpper().Contains(this.filter.ToUpper()))
            {
                this.filterRoom.Add(roomInfo);
            }
        }
    }

    private void Update()
    {
        if (Enabeled)
        {
            this.elapsedTime += Time.deltaTime;
            if (this.elapsedTime > 1f)
            {
                this.elapsedTime = 0f;
                this.showlist();
            }
        }
    }
}
