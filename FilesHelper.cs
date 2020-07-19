using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

	public class FilesHelper
	{
        public static string[] drivers;
        public static string current_catalog;
        public static List<string> catalogs;
        public static List<string> files;
        public static Texture2D main_texture;
        public static void getFilesAndDir(string path)
        {
            current_catalog = path;
            string[] sss = Directory.GetDirectories(path);
            catalogs = new List<string>();
            foreach (string ssd in sss)
            {
                string stp = ssd.Replace(@"\", "#");
                catalogs.Add(stp.Split(new char[] { '#' }).Last());
            }
            string[] fil = Directory.GetFiles(path);
            files = new List<string>();
            foreach (string ssd in fil)
            {

                string stp = ssd.Replace(@"\", "#");
                files.Add(stp.Split(new char[] { '#' }).Last());
            }
        }
        public class MyFiles
        {
            public readonly string path;
            public readonly bool isDirectory;
            public readonly bool isFile;
            public readonly int kbite;
            public readonly long bite;
         
            public MyFiles(string text)
            {
                isDirectory = Directory.Exists(text);
                FileInfo fi =  new FileInfo(text);
                isFile =fi.Exists;
                if (isFile)
                {
                    kbite = (int)(fi.Length / 1024);
                    bite = (fi.Length);
                }
                path = text;
            }
        }
	}

