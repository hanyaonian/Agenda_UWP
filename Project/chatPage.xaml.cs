using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Project
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class chatPage : Page
    {

        private ViewModels.ChatWindowViewModel ChatViewModels;
        public chatPage()
        {
            this.InitializeComponent();
            //got messagelist
            //show first message
        this.ChatViewModels = new ViewModels.ChatWindowViewModel();
        this.DataContext = this.ChatViewModels;
            //first message
        ChatViewModels.addMessage("robot", DateTime.Now, "有什么疑问或者想聊的可以和我说哦", false);

        }

        private async void GetMessage(string tel)
        {
            try
            {
                // 创建一个HTTP client实例对象
                HttpClient httpClient = new HttpClient();
                //http header
                var headers = httpClient.DefaultRequestHeaders;

                // The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
                // especially if the header value is coming from user input.
                string header = "ie Mozilla/5.0 (Windows NT 6.2; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";

                //添加apikey参数
                headers.Add("apikey", "11e371802fce595178b0c69274d2f07c");
                //默认标准key
                string key = "key=879a6cb3afb84dbf4fc84a1df2ab7319";

                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                //url
                string getMessage = "http://apis.baidu.com/turing/turing/turing" + "?" + key + "&info=" + tel;

                //发送GET请求
                HttpResponseMessage response = await httpClient.GetAsync(getMessage);

                // 确保返回值为成功状态
                response.EnsureSuccessStatusCode();

                // 因为返回的字节流中含有中文，传输过程中，所以需要编码后才可以正常显示
                Byte[] getByte = await response.Content.ReadAsByteArrayAsync();

                // UTF-8是Unicode的实现方式之一。这里采用UTF-8进行编码
                Encoding code = Encoding.GetEncoding("UTF-8");
                string result = code.GetString(getByte, 0, getByte.Length);

                JsonTextReader json = new JsonTextReader(new StringReader(result));
                string jsonVal = "", backMessage = "";

                // 获取回复
                while (json.Read())
                {
                    jsonVal += json.Value;
                    if (jsonVal.Equals("text"))  // 读到“text”时，取回复
                    {
                        json.Read();
                        backMessage += json.Value;  // 该对象重载了“+=”,故可与字符串进行连接
                        break;
                    }
                    jsonVal = "";
                }
                //robotResponse(backMessage);
                this.DataContext = null;
                ChatViewModels.addMessage("robot", DateTime.Now, backMessage, false);
                this.DataContext = this.ChatViewModels;
            }
            catch (HttpRequestException ex1)
            {
                var i = new MessageDialog(ex1.ToString()).ShowAsync();
            }
            catch (Exception ex2)
            {
                var i = new MessageDialog(ex2.ToString()).ShowAsync();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //userSpeak();
            this.DataContext = null;
            this.ChatViewModels.addMessage(App.login_user.Username, DateTime.Now, chatBox.Text.ToString(), true);
            this.DataContext = this.ChatViewModels;
            GetMessage(chatBox.Text.ToString());
        }

        //show userSpeak
        //private void userSpeak()
        //{
        //    //new a textblock
        //    TextBlock userMessage = new TextBlock();
        //    setRow();

        //    //get background image
        //    //ImageBrush background = new ImageBrush(new BitmapImage(new Uri()));
        //    //change textBlock margin
        //    userMessage.TextAlignment = TextAlignment.Left;

        //    userMessage.Text = App.login_user.Username +" :" + chatBox.Text.ToString();
        //    userMessage.FontSize = 24;
        //    robotGrid.Children.Add(userMessage);
        //    Grid.SetRow(userMessage, messageNum);
        //    messageNum = messageNum + 1;
        //}
        ////show robot response
        //private void robotResponse(string backMessage)
        //{
        //    TextBlock robotMessage = new TextBlock();
        //    setRow();
        //    robotMessage.TextAlignment = TextAlignment.Right;

        //    robotMessage.Text = "Robot :" + backMessage;
        //    robotMessage.FontSize = 24;
        //    robotGrid.Children.Add(robotMessage);
        //    Grid.SetRow(robotMessage, messageNum);
        //    messageNum = messageNum + 1;
        //}
        //private void setRow()
        //{
        //    //set row
        //    RowDefinition row = new RowDefinition();
        //    row.Height = new GridLength(50, GridUnitType.Pixel);
        //    robotGrid.RowDefinitions.Add(row);
        //}
    }
}
