using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;


namespace FoodStock01
{
    public partial class App : Application
    {

        //データベースのパスを格納
        public static string dbPath;

        public App(string dbPath)
        {
            //AppのdbPathに引数のパスを設定
            App.dbPath = dbPath;

            // TabbedPageをMainPageとしてセットする
            MainPage = new TabbedPage()
            {
                Children = {
                   new FoodNavi(new FoodPage1("食材リスト")),
                   new StockNavi(new StockPage1("保存食品リスト")),
                   new EntryNavi(new EntryPage1("登録")),
                   new MemoNavi(new MemoPage("メモ")),
                   new SettingNavi(new SettingPage("設定")),
                   //new RecipePage("レシピ")
                }
            };
        }

    }
}