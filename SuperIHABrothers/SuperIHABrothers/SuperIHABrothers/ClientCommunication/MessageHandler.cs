///////////////////////////////////////////////////////////
//  MessageHandler.cs
//  Implementation of the Class MessageHandler
//  Generated by Enterprise Architect
//  Created on:      07-maj-2013 09:39:14
//  Original author: Filip
///////////////////////////////////////////////////////////


using System.Threading;
using DampCommunication;
using Sprites;
using GameState;
using SuperIHABrothers.ClientCommunication;
using SuperIHABrothers.Sprite;

namespace DampCommunication {
	public class MessageHandler : IMessageHandler {

		public ISpriteContainerMessage m_ISpriteContainerMessage;
		public IGameState m_IGameState;
		public IMessageQueueRemove m_IMessageQueueRemove;

		public MessageHandler(ISpriteContainerMessage spriteContainerMessage, IGameState gameState, IMessageQueueRemove messageQueueRemove)
		{
		    m_ISpriteContainerMessage = spriteContainerMessage;
		    m_IGameState = gameState;
		    m_IMessageQueueRemove = messageQueueRemove;

            Thread myNewThread = new Thread(() => HandlerThread(m_IMessageQueueRemove));
            myNewThread.Start();
		}

        public void SetContainer(ISpriteContainerMessage mContainer)
        {
            m_ISpriteContainerMessage = mContainer;
        }

        private void HandlerThread(IMessageQueueRemove messageQueueRemove)
        {
            string message = messageQueueRemove.GetMessage();
            
            if (message != null)
            {
                switch (message)
                {
                    case "DOL":
                        m_IGameState.GameRunning = true;
                        break;
                        
                    case "DOF":
                        m_IGameState.GameRunning = false;
                        break;

                    default:
                        break;
                }
            }
        }

	   
	}//end MessageHandler

}//end namespace DampCommunication