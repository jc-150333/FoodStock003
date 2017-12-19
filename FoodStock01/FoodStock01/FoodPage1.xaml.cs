﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodStock01;
using System.Windows.Input;

namespace FoodStock01
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage1 : ContentPage
    {
        String s = "http://cookpad.com/search/";

        public FoodPage1(string title)
        {
            //タイトル
            Title = title;

            InitializeComponent();

            RefreshCommand = new Command(async (nothing) =>
            {   list.IsRefreshing = false;
                Title = "食材リスト";
                s = "http://cookpad.com/search/";

                InitializeComponent();
            },
            // ICommand.CanExecuteにもバインドしたプロパティを利用できる
                (nothing) => !list.IsRefreshing
            );
        }

        /***試し***/
        /*public FoodPage1(int insert)
        {
            if(insert == 1)
            {
                InitializeComponent();
            }
        }*/
        /***試し***/

        void ChackBoxChanged(object sender, bool isChecked)
        {
            //選択された時の処理
            if (isChecked)
            {
                s += ((CheckBox)sender).Text + "　";
            }
            //選択が外された時の処理
            else
            {
                s = s.Replace(((CheckBox)sender).Text + "　", "");
            }
        }

        void OnSearch_Clicked(object sender, EventArgs args)
        {
            //ページ遷移
            Navigation.PushAsync(new NextPage(s));
        }

        private void Update_Button_Clicked(object sender, EventArgs e)
        {
            Title = "食材リスト";
            s = "http://cookpad.com/search/";

            InitializeComponent();
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }
    }
}