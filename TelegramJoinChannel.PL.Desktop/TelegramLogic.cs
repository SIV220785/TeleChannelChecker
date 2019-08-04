using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramJoinChannel.PL.Desktop.Helper;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLChatFull = TeleSharp.TL.Messages.TLChatFull;

namespace TelegramJoinChannel.PL.Desktop
{
    public class TelegramLogic
    {

        private readonly int _apiId;
        private readonly string _apiHash;
        private string _hash;
        private readonly string _numberPhone;
        private readonly TelegramClient _client;

        private readonly List<TLChannel> _cannels;
        private List<TLChannelFull> _fullInfoCannels;

        public TLUser User { get; set; }

        //public TelegramClient Client { get; set; }

        public string Code { get; set; }


        public TelegramLogic(string numberPhone)
        {
            _numberPhone = numberPhone;
            _apiId = 855982;
            _apiHash = "14371f08963c3662ddb83073194b971c";
            _client = new TelegramClient(_apiId, _apiHash, new CustomSessionStore(_numberPhone), _numberPhone);

            _cannels = new List<TLChannel>();
            _fullInfoCannels = new List<TLChannelFull>();
        }

        public async Task LogInAsync()
        {
            await _client.ConnectAsync();
        }

        public async Task GetCodeAuthenticateAsync()
        {
            await _client.ConnectAsync();
            _hash = await _client.SendCodeRequestAsync(_numberPhone);
        }

        public async Task AuthUserAsync()
        {

            User = await _client.MakeAuthAsync(_numberPhone, _hash, Code);
            Assert.IsNotNull(User);
            Assert.IsTrue(_client.IsUserAuthorized());
        }

        public async Task<List<TLChannel>> GetInfoChannelsAsync()
        {
            _cannels.Clear();
            var dialogs = (TLDialogs)await _client.GetUserDialogsAsync();
            foreach (var item in dialogs.Chats)
            {
                if (item is TLChannel)
                {
                    TLChannel chat = item as TLChannel;
                    _cannels.Add(chat);
                }
            }
            return _cannels;
        }

        public async Task<List<TLChannelFull>> GetFullInfoCannelsAsync()
        {
            _fullInfoCannels.Clear();
            var dialogs = (TLDialogs)await _client.GetUserDialogsAsync();

            foreach (var item in dialogs.Chats)
            {
                if (item is TLChannel)
                {
                    TLChannel chat = item as TLChannel;
                    var chan = await _client.SendRequestAsync<TLChatFull>(new TLRequestGetFullChannel()
                    {
                        Channel = new TLInputChannel()
                        { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash }
                    });

                    TLChannelFull tLChannelFull = chan.FullChat as TLChannelFull;
                    _fullInfoCannels.Add(tLChannelFull);
                }
            }
            return _fullInfoCannels;
        }

        public async Task<bool> CheckUriChannel(long accessHash, int id)
        {
            TLChatFull chan = new TLChatFull();
            try
            {
                chan = await _client.SendRequestAsync<TLChatFull>(new TLRequestGetFullChannel()
                {
                    Channel = new TLInputChannel()
                    { ChannelId = id, AccessHash = accessHash }
                });
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public async Task<int> JoinCannel(string channelName)
        {

            var channelInfo = (await _client.SendRequestAsync<TeleSharp.TL.Contacts.TLResolvedPeer>(
             new TeleSharp.TL.Contacts.TLRequestResolveUsername
             {
                 Username = channelName,

             }).ConfigureAwait(false)).Chats[0] as TeleSharp.TL.TLChannel;
            
            var Request = new TeleSharp.TL.Channels.TLRequestJoinChannel()
            {
                Channel = new TLInputChannel
                {
                    ChannelId = channelInfo.Id,
                    AccessHash = (long)channelInfo.AccessHash
                }
            };

            try
            {
                var Respons = await _client.SendRequestAsync<Boolean>(Request);
            }
            catch (Exception)
            {
            }
            return channelInfo.Id;
        }
    }
}
