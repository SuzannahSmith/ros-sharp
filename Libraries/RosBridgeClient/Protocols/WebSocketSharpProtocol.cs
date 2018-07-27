﻿/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using WebSocketSharp;

namespace RosSharp.RosBridgeClient.Protocols
{
    public class WebSocketSharpProtocol: Protocol
    {
        public override event EventHandler OnReceive;

        private WebSocket WebSocket;

        public WebSocketSharpProtocol(string url)
        {
            WebSocket = new WebSocket(url);
            WebSocket.OnMessage += Receive;                
        }
                
        public override void Connect()
        {
            WebSocket.Connect();            
        }

        public override void Close()
        {
            WebSocket.Close();
        }

        public override bool IsAlive()
        {
            return WebSocket.IsAlive;
        }

        public override void Send(byte[] data)
        {
            WebSocket.SendAsync(data, null);
        }
        
        private void Receive(object sender, WebSocketSharp.MessageEventArgs e)
        {
            OnReceive.Invoke(sender, new MessageEventArgs(e.RawData));
        }
    }
}
