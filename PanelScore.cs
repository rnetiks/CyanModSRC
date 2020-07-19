using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

	public class PanelScore 
	{
        public static List<PanelScore> list;
        public static int active = 0;
        public List<Stats> titam_list;
        public float timeSession;
        public string nickname;
        public string time;
        public string difficulty;
        public bool isMuilty;
        public LevelInfo levelinfo;
        public static void Clear()
        {
            list.Clear();
        }
        public PanelScore(string nick,LevelInfo level)
        {
            if (list == null)
            {
                list = new List<PanelScore>();
            }
            nickname = nick;
            time = DateTime.Now.ToString("HH:mm:ss");
            titam_list = new List<Stats>();
            timeSession = 0f;
            isMuilty = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER;
            difficulty = IN_GAME_MAIN_CAMERA.difficulty.ToString();
            list.Add(this);
            if (list.Count > 50)
            {
                list.Remove(list[0]); 
            }
            levelinfo = level;
        }
        public void AddStats(string name, int damage,float size =0f)
        {
            titam_list.Add(new Stats(name, damage, timeSession, size));
        }
        public void AddDeath()
        {
            titam_list.Add(new Stats(timeSession));
        }
        public class Stats
        {
            public string titan_name;
            public string time;
            public float sessiontime;
            public int damage;
            public float size = 0f;
            public bool death = false;
            public Stats(string n, int d,float ses_t,  float s = 0f)
            {
                damage = d;
                titan_name = n;
                size = s;
                time = DateTime.Now.ToString("HH:mm:ss");
                sessiontime = ses_t;
            }
            public Stats(float ses_t)
            {
                death = true;
                time = DateTime.Now.ToString("HH:mm:ss");
                sessiontime = ses_t;
            }
        }
	}

