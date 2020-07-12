using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Base
{
    public struct CheckConnectionJob : IJob
    {
        public int timeOut;
        private bool hasConnection;
        public NativeArray<bool> result;

        public void Execute()
        {
            string html = String.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Constant.URL.Google);
                request.Method = "GET";
                request.Timeout = timeOut;
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    bool isSuccess = (int)response.StatusCode < 299 && (int)response.StatusCode >= 200;
                    if (isSuccess)
                    {
                        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                        {
                            char[] cs = new char[80];
                            stream.Read(cs, 0, cs.Length);
                            foreach (var ch in cs)
                            {
                                html += ch;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                html = "";
            }

            if (html.Length > 0)
            {
                hasConnection = true;
            }
            else
            {
                hasConnection = false;
            }
            
            result[0] = hasConnection;
        }
    }
}

