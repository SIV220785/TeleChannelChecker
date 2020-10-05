using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.Interfaces;
using TelegramJoinChannel.PL.Desktop.Model;
using TelegramJoinChannel.PL.Desktop.ReleyCommand;
using TelegramJoinChannel.PL.Desktop.Services;
using TeleSharp.TL;
using Unity;

namespace TelegramJoinChannel.PL.Desktop.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly UnityContainer _container;
        private readonly ChannelsService _channelsService;
        private readonly CheckStatusService _checkStatusService;

        private TelegramLogic _telegramLogic;
        private string _path;
        private string _numberPhone;
        private string _loginResult;
        private string _codeToAuthenticate;
        private readonly List<string> _files;
        private readonly List<ChannelsInfo> _updateChannelsBD;

        private DateTime _lastCheck;
        private int _noLinks;
        private int _updatedAvatar;
        private int _changeTitles;
        private int _subscribersChannel;
        private string _inputChannelName;


        private bool _isLinkDoesNotWork;
        private bool _isNameChanged;
        private bool _isUpdatedAvatar;
        private bool _isSetCode;


        private string _uriChannel;
        private int _successfullyAdded;
        private int _repeatedChannel;
        private int _failedGetData;

        private List<TLChannelFull> _tLChannelFull;
        private List<TLChannel> _tLChannels;


        public ICommand SetCode { get; set; }
        public ICommand Authenticate { get; set; }
        public ICommand CheckMarked { get; set; }
        public ICommand CheckAll { get; set; }
        public ICommand DeleteSelected { get; set; }
        public ICommand Search { get; set; }
        public ICommand AddChannel { get; set; }
        public ICommand UpdateChannale { get; set; }


        public ObservableCollection<AddChannels> UriList { get; set; }
        public ObservableCollection<ChannelsInfo> InfoChannels { get; set; }
        public ObservableCollection<CheckStatusInfo> StatChannels { get; set; }

        public string InputUriChannel
        {
            get { return _inputChannelName; }
            set { _inputChannelName = value; OnPropertyChanged(); }
        }
        public string LoginResult
        {
            get { return _loginResult; }
            set { _loginResult = value; OnPropertyChanged(); }
        }

        public int ChangeTitles
        {
            get { return _changeTitles; }
            set { _changeTitles = value; OnPropertyChanged(); }
        }
        public int UpdatedAvatar
        {
            get { return _updatedAvatar; }
            set { _updatedAvatar = value; OnPropertyChanged(); }
        }
        public int SubscribersChannel
        {
            get { return _subscribersChannel; }
            set { _subscribersChannel = value; OnPropertyChanged(); }
        }
        public int NoLinks
        {
            get { return _noLinks; }
            set { _noLinks = value; OnPropertyChanged(); }
        }
        public DateTime LastCheck
        {
            get { return _lastCheck; }
            set { _lastCheck = value; OnPropertyChanged(); }
        }
        public string NumberPhone
        {
            get { return _numberPhone; }
            set { _numberPhone = value; OnPropertyChanged(); }
        }
        public string CodeToAuthenticate
        {
            get { return _codeToAuthenticate; }
            set { _codeToAuthenticate = value; OnPropertyChanged(); }
        }
        public bool IsUpdatedAvatar
        {
            get { return _isUpdatedAvatar; }
            set { _isUpdatedAvatar = value; OnPropertyChanged(); }
        }
        public bool IsNameChanged
        {
            get { return _isNameChanged; }
            set { _isNameChanged = value; OnPropertyChanged(); }
        }
        public bool IsLinkDoesNotWork
        {
            get { return _isLinkDoesNotWork; }
            set { _isLinkDoesNotWork = value; OnPropertyChanged(); }
        }
        public bool IsSetCode
        {
            get { return _isSetCode; }
            set { _isSetCode = value; OnPropertyChanged(); }
        }

        private bool _isAuthorisation;

        public bool IsAuthorisation
        {
            get { return _isAuthorisation; }
            set { _isAuthorisation = value; OnPropertyChanged(); }
        }

        public string UriChannel
        {
            get { return _uriChannel; }
            set { _uriChannel = value; OnPropertyChanged(); }
        }
        public int SuccessfullyAdded
        {
            get { return _successfullyAdded; }
            set { _successfullyAdded = value; OnPropertyChanged(); }
        }
        public int RepeatedChannel
        {
            get { return _repeatedChannel; }
            set { _repeatedChannel = value; OnPropertyChanged(); }
        }
        public int FailedGetData
        {
            get { return _failedGetData; }
            set { _failedGetData = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            _container = new UnityContainer();
            _container.RegisterType<ICheckStatusService, TelegramJoinChannel.BLL.Services.CheckStatusService>();
            _container.RegisterType<IChannelsService, TelegramJoinChannel.BLL.Services.ChannelsService>();
            IsAuthorisation = true;
            _channelsService = new ChannelsService(_container);
            _checkStatusService = new CheckStatusService(_container);
            InfoChannels = _channelsService.GetChannelsInfo();
            StatChannels = _checkStatusService.GetCheckChannelsInfo();
            if (StatChannels.Count != 0)
            {
                LastCheck = StatChannels.Last().CheckDate;
            }

            SetCode = new RelayCommand(OnSetCode);
            Authenticate = new RelayCommand(OnAuthenticate);
            CheckMarked = new RelayCommand(OnCheckMarked);
            CheckAll = new RelayCommand(OnCheckAll);
            DeleteSelected = new RelayCommand(OnDeleteSelected);
            Search = new RelayCommand(OnSearch);
            UpdateChannale = new RelayCommand(OnUpdateChannale);

            AddChannel = new RelayCommand(OnAddChannel);

            _files = new List<string>();
            _updateChannelsBD = new List<ChannelsInfo>();

            _tLChannelFull = new List<TLChannelFull>();
            _tLChannels = new List<TLChannel>();

            UriList = new ObservableCollection<AddChannels>();

            _numberPhone = "+375293232203";
            Application.Current.MainWindow.Closing += MainWindow_Closing;
        }


        private async void OnAuthenticate(object obj)
        {
            await Task.Run(async () =>
            {
                IsAuthorisation = false;
                IsSetCode = false;
                _telegramLogic = new TelegramLogic(NumberPhone);

                _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sessions");
                var tmpfiles = Directory.GetFiles(_path, "*", SearchOption.AllDirectories).ToList();

                foreach (var item in tmpfiles)
                {
                    _files.Add(Path.GetFileName(item));
                }
                foreach (var item in _files)
                {
                    if (item.Equals(NumberPhone + ".dat"))
                    {
                        await _telegramLogic.LogInAsync();
                        LoginResult = "Вы вошли успешно!!! ";
                        return;
                    }
                }

                await _telegramLogic.GetCodeAuthenticateAsync();
                LoginResult = "Ждите код для подтверждения";
                IsSetCode = true;
            });
        }

        private async void OnSetCode(object obj)
        {
            await Task.Run(async () =>
            {
                IsSetCode = false;
                _telegramLogic.Code = CodeToAuthenticate;
                await _telegramLogic.AuthUserAsync();
                LoginResult = "Вы вошли успешно!!! ";
                CodeToAuthenticate = String.Empty;
            });

        }

        private async void OnAddChannel(object obj)
        {
            await Task.Run(async () =>
            {
                string res = "";
                try
                {
                    res = InputUriChannel.Split('/').Last().Split('@').Last();
                }
                catch (Exception)
                {
                }

                try
                {
                    var id = await _telegramLogic.JoinCannel(res);
                    _tLChannelFull = await _telegramLogic.GetFullInfoCannelsAsync();
                    _tLChannels = await _telegramLogic.GetInfoChannelsAsync();

                    var tLChannelFullinfo = _tLChannelFull.Where(p => p.Id == id).FirstOrDefault();
                    var tLChannels = _tLChannels.Where(p => p.Id == id).FirstOrDefault();


                    var channelsInfo = new ChannelsInfo()
                    {
                        SubscribersChannel = (int)tLChannelFullinfo.ParticipantsCount,
                        UriChannel = tLChannels.Username,
                        NameChannel = tLChannels.Title,
                        IdChannel = tLChannels.Id,
                        AccessHashChannel = (long)tLChannels.AccessHash,
                        FullUriCannel = InputUriChannel
                    };

                    channelsInfo.AvatarChannel = tLChannelFullinfo.ChatPhoto is TLPhoto tLPhoto
                        ? DateTimeOffset.FromUnixTimeSeconds(tLPhoto.Date).DateTime
                        : new DateTime(2000, 1, 1);

                    InfoChannels.Add(_channelsService.Create(channelsInfo));
                    AddChannels addChannels = new AddChannels
                    {
                        UriChannel = InputUriChannel
                    };
                    UriList.Add(addChannels);
                }
                catch (Exception)
                { }
            });
        }


        private async void OnCheckAll(object obj)
        {
            NoLinks = 0;
            UpdatedAvatar = 0;
            ChangeTitles = 0;
            SubscribersChannel = 0;

            _tLChannelFull = await _telegramLogic.GetFullInfoCannelsAsync();
            _tLChannels = await _telegramLogic.GetInfoChannelsAsync();
            List<ChannelsInfo> cannelsTelegram = new List<ChannelsInfo>();
            foreach (var item in _tLChannelFull)
            {
                var channelsInfoTelegram = new ChannelsInfo();
                foreach (var item1 in _tLChannels)
                {
                    if (item.Id == item1.Id)
                    {
                        channelsInfoTelegram.NameChannel = item1.Title;
                        channelsInfoTelegram.UriChannel = item1.Username;
                        channelsInfoTelegram.IdChannel = item1.Id;
                        channelsInfoTelegram.AccessHashChannel = (long)item1.AccessHash;
                        break;
                    }
                }
                if (item.ChatPhoto is TLPhoto tLPhoto)
                    channelsInfoTelegram.AvatarChannel = DateTimeOffset.FromUnixTimeSeconds(tLPhoto.Date).DateTime;
                else
                    channelsInfoTelegram.AvatarChannel = new DateTime(2000, 1, 1);
                channelsInfoTelegram.SubscribersChannel = (int)item.ParticipantsCount;
                cannelsTelegram.Add(channelsInfoTelegram);
            }

            string resultStatus = String.Empty;
            foreach (var itemInfoChannels in InfoChannels)
            {
                var isCheck = await _telegramLogic.CheckUriChannel(itemInfoChannels.AccessHashChannel, itemInfoChannels.IdChannel);

                if (!isCheck)
                {
                    resultStatus += $"Ссылка не действительна. ";
                    NoLinks++;
                    var tmpChannel = itemInfoChannels;
                    tmpChannel.Status = resultStatus;
                    _updateChannelsBD.Add(tmpChannel);
                    resultStatus = String.Empty;
                    continue;
                }

                foreach (var itemCannelsTelegram in cannelsTelegram)
                {
                    if (itemInfoChannels.IdChannel == itemCannelsTelegram.IdChannel)
                    {


                        if (DateTime.Compare(itemInfoChannels.AvatarChannel, itemCannelsTelegram.AvatarChannel) != 0)
                        {
                            resultStatus += $"Изменили аватар. ";
                            itemInfoChannels.AvatarChannel = itemCannelsTelegram.AvatarChannel;
                            UpdatedAvatar++;
                        }

                        if (!itemInfoChannels.NameChannel.Equals(itemCannelsTelegram.NameChannel))
                        {
                            resultStatus += $"Изменили имя группы. ";
                            itemInfoChannels.NameChannel = itemCannelsTelegram.NameChannel;
                            ChangeTitles++;
                        }

                        if (itemInfoChannels.SubscribersChannel != itemCannelsTelegram.SubscribersChannel)
                        {
                            resultStatus += $"Изменилось количество подписчиков группы. ";
                            itemInfoChannels.SubscribersChannel = itemCannelsTelegram.SubscribersChannel;
                            SubscribersChannel++;
                        }

                        var tmp = itemInfoChannels;
                        tmp.Status = resultStatus;
                        _updateChannelsBD.Add(tmp);
                        resultStatus = String.Empty;
                    }
                }
            }
            foreach (var item in _updateChannelsBD)
            {
                InfoChannels.Remove(item);
                InfoChannels.Add(item);
            }
            CheckStatusInfo checkStatusInfo = new CheckStatusInfo()
            {
                CheckDate = DateTime.Now,
                ChangeAvatar = UpdatedAvatar,
                ChangeName = ChangeTitles,
                ChannelsCount = SubscribersChannel,
                NoUri = NoLinks
            };
            StatChannels.Add(_checkStatusService.Create(checkStatusInfo));
        }

        private async void OnUpdateChannale(object obj)
        {

            _tLChannelFull = await _telegramLogic.GetFullInfoCannelsAsync();
            _tLChannels = await _telegramLogic.GetInfoChannelsAsync();

            List<ChannelsInfo> cannelsTelegram = new List<ChannelsInfo>();

            foreach (var item in _tLChannelFull)
            {
                var channelsInfo = new ChannelsInfo();
                foreach (var item1 in _tLChannels)
                {
                    if (item.Id == item1.Id)
                    {
                        channelsInfo.NameChannel = item1.Title;
                        channelsInfo.UriChannel = item1.Username;
                        channelsInfo.IdChannel = item1.Id;
                        channelsInfo.AccessHashChannel = (long)item1.AccessHash;
                        break;
                    }
                }
                if (item.ChatPhoto is TLPhoto tLPhoto)
                    channelsInfo.AvatarChannel = DateTimeOffset.FromUnixTimeSeconds(tLPhoto.Date).DateTime;
                else
                    channelsInfo.AvatarChannel = new DateTime(2000, 1, 1);
                channelsInfo.SubscribersChannel = (int)item.ParticipantsCount;
                cannelsTelegram.Add(channelsInfo);
            }

            List<ChannelsInfo> tmpList = new List<ChannelsInfo>();
            foreach (var tLChannelItem in cannelsTelegram)
            {

                foreach (var infoChannelsItem in InfoChannels)
                {
                    if (infoChannelsItem.IdChannel == tLChannelItem.IdChannel)
                    {
                        tmpList.Add(tLChannelItem);
                    }
                }
            }
            foreach (var item in tmpList)
            {
                cannelsTelegram.Remove(item);
            }
            foreach (var item in cannelsTelegram)
            {
                InfoChannels.Add(_channelsService.Create(item));
            }

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var item in _updateChannelsBD)
            {
                item.Status = String.Empty;
                _channelsService.Updete(item);
            }
        }

        private void OnCheckMarked(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnDeleteSelected(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnSearch(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
