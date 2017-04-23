using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Project
{
    class MessageItemDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Message)
            {
                if ((item as Message).IsSelf)
                {
                    return App.Current.Resources["SelfMessageDataTemplate"] as DataTemplate;
                }
                else
                {
                    return App.Current.Resources["MessageDataTemplate"] as DataTemplate;
                }
            }

            return base.SelectTemplateCore(item);
        }
    }
}
