using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FoodStock01
{
    public interface INotificationService
    {
        //iOS用の登録
        void Regist();
        
        //通知する
        void On(string title,string subTitle,string body);

        //通知を解除する
        void Off();

        //0にする
        void return0();
        void On(string v1, string v2, List<FoodModel> list);
    }
}