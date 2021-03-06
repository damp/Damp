﻿/**
 * @file   	RequestProcessor.cs
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file implements the command processor for DAMP Server
 * @section	LICENSE GPL 
 */

#region

using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DampServer.commands;
using DampServer.exceptions;
using DampServer.interfaces;

#endregion

namespace DampServer
{
    /**
    * @brief RequestProcessor Constructor
    */
    public class RequestProcessor
    {
        private readonly TcpClient _socket;

        public RequestProcessor(TcpClient s)
        {
            _socket = s;

            Task.Factory.StartNew(Run);
        }

        public void Run()
        {
            Console.WriteLine("Got Connection Proccesing it!");
            Http hp;

            try
            {
                hp = new Http(_socket);
            }
            catch (InvalidHttpRequestException e)
            {
                Console.WriteLine("Exception 2: Closing connection: {0}", e.Message);
                CloseSocket();
                return;
            }

            if (string.IsNullOrEmpty(hp.Path))
            {
                Console.WriteLine("Request has no command!");
                ErrorXmlResponse r = new ErrorXmlResponse {Date = new DateTime(), Message = "Request has no command!"};
                hp.SendXmlResponse(r);
                CloseSocket();
            }

            Console.WriteLine("Looking for Command handler for {0}", hp.Path);

            try
            {
                IServerCommand serverCommand = CreateCommand(hp.Path);
                Console.WriteLine("CommandProcesser.FoundCommand for {0}", hp.Path);


                if (serverCommand.NeedsAuthcatication)
                {
                    if (string.IsNullOrEmpty(hp.Query.Get("AuthToken")) ||
                        !CheckAuthetication(hp.Query.Get("AuthToken")))
                    {
                        ErrorXmlResponse r = new ErrorXmlResponse {Date = new DateTime(), Message = "Access Denied"};
                        hp.SendXmlResponse(r);

                        CloseSocket();
                        return;
                    }
                }

                serverCommand.Execute(hp, hp.Path);

                if (!serverCommand.IsPersistant) CloseSocket();
                else
                {
                    Console.WriteLine("Not Closing Connection is persistant!");
                }
            }
            catch (CommandNotFoundException e)
            {
                ErrorXmlResponse r = new ErrorXmlResponse
                    {
                        Date = new DateTime(),
                        Message = "Command not found! " + e.Message
                    };
                hp.SendXmlResponse(r);
                CloseSocket();
                Console.WriteLine("Command not found!");
            }
            catch (InvalidHttpRequestException e)
            {
                ErrorXmlResponse r = new ErrorXmlResponse {Message = e.Message};
                hp.SendXmlResponse(r);
                CloseSocket();
            }
            catch (InvalidFileHashException e)
            {
                Logger.Log(e.Message);
                hp.SendXmlResponse(new ErrorXmlResponse {Message = "Internal Server Error! #1337 " + e.Message});
                CloseSocket();
            }
        }

        private void CloseSocket()
        {
            Console.WriteLine("Disconnect and close socket");
            //     _socket.Disconnect(false);
            _socket.Close();
        }

        private static bool CheckAuthetication(string authToken)
        {
            try
            {
                UserManagement.GetUserByAuthToken(authToken);
                return true;
            }
            catch (UserNotFoundException)
            {
                return false;
            }
        }

        /**
         * CreateCommand
         * 
         * @brief returns a command object to handle a given cmd
         * @param string cmd cmd to handle
         * @return IServerCommand object that handles the given command
         */
        public static IServerCommand CreateCommand(string cmd)
        {
            IServerCommand leCmd;
            switch (cmd)
            {
                case "Chat":
                    leCmd = new ChatCommand();
                    break;
                case "Login":
                    leCmd = new LoginCommand();
                    break;
                case "Live":
                    leCmd = new LiveCommand();
                    break;
                case "UploadGame":
                    leCmd = new UploadGameCommand();
                    break;
                case "GetUser":
                    leCmd = new UserCommands();
                    break;
                case "GetMyUser":
                    leCmd = new UserCommands();
                    break;
                case "AddFriend":
                    leCmd = new FriendCommand();
                    break;
                case "AcceptFriend":
                    leCmd = new FriendCommand();
                    break;
                case "ForgottenPassword":
                    leCmd = new UserCommands();
                    break;
                case "RemoveFriend":
                    leCmd = new FriendCommand();
                    break;
                case "FriendSearch":
                    leCmd = new UserCommands();
                    break;
                case "Download":
                    leCmd = new DownloadCommand();
                    break;
                case "GetMyGames":
                    leCmd = new GameCommand();
                    break;
                case "GetAllGames":
                    leCmd = new GameCommand();
                    break;
                case "GameSearch":
                    leCmd = new GameCommand();
                    break;
                case "BuyGame":
                    leCmd = new GameCommand();
                    break;
                case "GameInfo":
                    leCmd = new GameCommand();
                    break;
                case "GetGame":
                    leCmd = new GameCommand();
                    break;
                case "AddAchievement":
                    leCmd = new AchievementCommand();
                    break;
                case "GetAllMyAchievement":
                    leCmd = new AchievementCommand();
                    break;
                case "AchievementSearch":
                    leCmd = new AchievementCommand();
                    break;
                case "GetAchievementsForGame":
                    leCmd = new AchievementCommand();
                    break;
                case "GetAllUserAchievements":
                    leCmd = new AchievementCommand();
                    break;
                case "GetUserAchievements":
                    leCmd = new AchievementCommand();
                    break;
                case "GetGameMyAchievements":
                    leCmd = new AchievementCommand();
                    break;
                case "UpdateScore":
                    leCmd = new HighScoreCommand();
                    break;
                case "GetScore":
                    leCmd = new HighScoreCommand();
                    break;
                case "GetHighScore":
                    leCmd = new HighScoreCommand();
                    break;
                case "AddUser":
                    leCmd = new AddUserCommand();
                    break;
                default:
                    throw new CommandNotFoundException(cmd);
            }

            return leCmd;
        }
    }
}