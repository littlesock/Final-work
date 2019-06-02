using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xblocks
{
    public partial class Form1 : Form
    {
        Label[,] grids = new Label[20,10]; //遊戲範圍 黑框
        Label[,] next = new Label[4, 3];   //提示框範圍 白框
        bool[,] signs = new bool[24,10];   //積木出現範圍
        Color[,] grids_color = new Color[24,10];  //積木顏色出現範圍
        uint block_row = 20;//積木初始高度位置
        uint block_col = 4;//積木初始位於第5行
        uint block_type ;//積木類型
        uint block_row_pre = 20;//積木上一步的高度
        uint block_col_pre = 4;//積木上一步位於第幾行
        uint block_type_pre ;//積木之前的類型、模組
        uint block_type_next;//積木下一個類型、模組
        bool block_changed = false;//積木能不能變形
        Random rander = new Random(System.DateTime.Now.Millisecond);
        int timer_interval = 1010;//積木降下的時間
        int game_mode = 1;//遊戲模式
        uint block_count = 0;//積木個數
        uint score = 0;//分數

        public Form1()
        {
            InitializeComponent();
            block_type = (uint)rander.Next(0, 7) + 1;//產生1～7之間的亂數
            block_type_pre = block_type;//因為是第一個，所以指定為跟現在一樣的類型
            block_type_next= block_type;//因為是第一個，所以沒有之後的類型

            for (int i = 0; i < 20; i++)//遊戲範圍 黑框
                for (int j = 0; j < 10; j++)
                {
                    grids[i, j] = new Label();
                    grids[i, j].Width = 30;//方塊寬度
                    grids[i, j].Height = 30;//方塊高度
                    grids[i, j].BorderStyle = BorderStyle.FixedSingle;//單行邊線
                    grids[i, j].BackColor = Color.Black;//顏色為黑色                    
                    grids[i, j].Left = 150 + 30 * j;
                    grids[i, j].Top = 600 - i * 30;
                    grids[i, j].Visible = true;
                    this.Controls.Add(grids[i, j]);
                }

            for (int i = 0; i < 4; i++)//提示框範圍 白框
                for (int j = 0; j < 3; j++)
                {
                    next[i, j] = new Label();
                    next[i, j].Width = 20;
                    next[i, j].Height = 20;
                    next[i, j].BorderStyle = BorderStyle.FixedSingle;
                    next[i, j].BackColor = Color.White;                    
                    next[i, j].Left = 515 + 20 * j; 
                    next[i, j].Top = 150 - i * 20;
                    next[i, j].Visible = true;
                    this.Controls.Add(next[i, j]);
                }
            
            init_game();
            
            System.Media.SoundPlayer player =new System.Media.SoundPlayer();//音效
            player.SoundLocation = "xblocks.wav";
            player.Load();
            player.PlayLooping();

        }

        void init_game()  //初始化
        {
            block_type = (uint)rander.Next(0, 7) + 1;
            block_type_pre = block_type;
            block_row = 20;
            block_col = 4;
            block_row_pre = 20;
            block_col_pre = 4;
            block_type_pre = block_type;
            block_type_next = block_type;
            block_changed = false;
            timer_interval = 1010;
            timer1.Interval =  timer_interval;
            block_count = 0;
            score = 0;
            game_mode = 1;

            for (uint i = 0; i < 24; i++)      //初始化積木模組
                for (uint j = 0; j < 10; j++)
                    signs[i, j] = false;
        }

        void update_block(uint i, uint j, uint type)//積木模組，共37組
        {
            switch (type)
            {
                case 1: 
                    signs[i, j] = signs[i+1, j] = signs[i+2, j] = signs[i+3, j] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i + 2, j] = grids_color[i + 3, j] = Color.Blue;
                    break;
                case 11: 
                    signs[i, j] = signs[i, j+1] = signs[i, j+2] = signs[i, j+3] = true;
                    grids_color[i, j] = grids_color[i, j + 1] = grids_color[i, j + 2] = grids_color[i, j + 3] = Color.Blue;
                    break;
               case 2:
                    signs[i, j] = signs[i + 1, j] = signs[i , j+1] = signs[i + 1, j+1] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i, j + 1] = grids_color[i + 1, j + 1] = Color.Yellow;
                    break;
               case 3:
                    signs[i, j] = signs[i + 1, j] = signs[i+1, j-1] = signs[i, j + 1] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i + 1, j - 1] = grids_color[i, j + 1] = Color.Red;
                    break;
               case 13:
                    signs[i, j] = signs[i - 1, j] = signs[i , j + 1] = signs[i+1, j + 1] = true;
                    grids_color[i, j] = grids_color[i - 1, j] = grids_color[i, j + 1] = grids_color[i + 1, j + 1] = Color.Red;
                    break;
               case 4:
                    signs[i, j] = signs[i , j-1] = signs[i + 1, j] = signs[i+1, j + 1] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i + 1, j] = grids_color[i + 1, j + 1] = Color.Green;
                    break;
               case 14:
                    signs[i, j] = signs[i+1, j] = signs[i, j+1] = signs[i-1, j + 1] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i, j + 1] = grids_color[i - 1, j + 1] = Color.Green;
                    break;
               case 5:
                    signs[i, j] = signs[i+1, j] = signs[i + 1, j+1] = signs[i + 1, j + 2] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i + 1, j + 1] = grids_color[i + 1, j + 2] = Color.Orange;
                    break;
               case 15:
                    signs[i, j] = signs[i, j-1] = signs[i + 1, j - 1] = signs[i + 2, j -1] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i + 1, j - 1] = grids_color[i + 2, j - 1] = Color.Orange;
                    break;
               case 25:
                    signs[i, j] = signs[i-1, j] = signs[i - 1, j - 1] = signs[i -1, j - 2] = true;
                    grids_color[i, j] = grids_color[i - 1, j] = grids_color[i - 1, j - 1] = grids_color[i - 1, j - 2] = Color.Orange;
                    break;
               case 35:
                    signs[i, j] = signs[i, j+1] = signs[i - 1, j + 1] = signs[i - 2, j +1] = true;
                    grids_color[i, j] = grids_color[i, j + 1] = grids_color[i - 1, j + 1] = grids_color[i - 2, j + 1] = Color.Orange;
                    break;
               case 6:
                    signs[i, j] = signs[i + 1, j] = signs[i + 1, j - 1] = signs[i + 1, j - 2] = true;
                    grids_color[i, j] = grids_color[i + 1, j] = grids_color[i + 1, j - 1] = grids_color[i + 1, j - 2] = Color.LightBlue;
                    break;
               case 16:
                    signs[i, j] = signs[i, j+1] = signs[i + 1, j + 1] = signs[i + 2, j + 1] = true;
                    grids_color[i, j] = grids_color[i, j + 1] = grids_color[i + 1, j + 1] = grids_color[i + 2, j + 1] = Color.LightBlue;
                    break;
               case 26:
                    signs[i, j] = signs[i-1, j] = signs[i-1, j + 1] = signs[i -1, j + 2] = true;
                    grids_color[i, j] = grids_color[i - 1, j] = grids_color[i - 1, j + 1] = grids_color[i - 1, j + 2] = Color.LightBlue;
                    break;
               case 36:
                    signs[i, j] = signs[i, j-1] = signs[i - 1, j - 1] = signs[i - 2, j -1] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i - 1, j - 1] = grids_color[i - 2, j - 1] = Color.LightBlue;
                    break;

               case 7:
                    signs[i, j] = signs[i, j-1] = signs[i, j+1] = signs[i+1, j] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i, j + 1] = grids_color[i + 1, j] = Color.Purple;
                    break;
               case 17:
                    signs[i, j] = signs[i, j + 1] = signs[i-1, j] = signs[i + 1, j] = true;
                    grids_color[i, j] = grids_color[i, j + 1] = grids_color[i - 1, j] = grids_color[i + 1, j] = Color.Purple;
                    break;
               case 27:
                    signs[i, j] = signs[i, j - 1] = signs[i, j+1] = signs[i - 1, j] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i, j + 1] = grids_color[i - 1, j] = Color.Purple;
                    break;
               case 37:
                    signs[i, j] = signs[i, j - 1] = signs[i+1, j] = signs[i - 1, j] = true;
                    grids_color[i, j] = grids_color[i, j - 1] = grids_color[i + 1, j] = grids_color[i - 1, j] = Color.Purple;
                    break;
            }
        }

        void erase_block(uint i, uint j, uint type)//消除積木先前所在位置
        {
            switch (type)
            {
                case 1:
                    signs[i, j] = signs[i + 1, j] = signs[i + 2, j] = signs[i + 3, j] = false;                    
                    break;
                case 11:
                    signs[i, j] = signs[i, j+1] = signs[i, j+2] = signs[i, j+3] = false;
                    break;
                case 2:
                    signs[i, j] = signs[i + 1, j] = signs[i , j+1] = signs[i + 1, j+1] = false;
                    break;
                case 3:
                    signs[i, j] = signs[i + 1, j] = signs[i + 1, j - 1] = signs[i, j + 1] = false;
                    break;
                case 13:
                    signs[i, j] = signs[i - 1, j] = signs[i, j + 1] = signs[i + 1, j + 1] = false;
                    break;
                case 4:
                    signs[i, j] = signs[i, j - 1] = signs[i + 1, j] = signs[i + 1, j + 1] = false;
                    break;
                case 14:
                    signs[i, j] = signs[i + 1, j] = signs[i, j + 1] = signs[i - 1, j + 1] = false;
                    break;
                case 5:
                    signs[i, j] = signs[i + 1, j] = signs[i + 1, j + 1] = signs[i + 1, j + 2] = false;
                    break;
                case 15:
                    signs[i, j] = signs[i, j - 1] = signs[i + 1, j - 1] = signs[i + 2, j - 1] = false;
                    break;
                case 25:
                    signs[i, j] = signs[i - 1, j] = signs[i - 1, j - 1] = signs[i - 1, j - 2] = false;
                    break;
                case 35:
                    signs[i, j] = signs[i, j + 1] = signs[i - 1, j + 1] = signs[i - 2, j + 1] = false;
                    break;
                case 6:
                     signs[i, j] = signs[i + 1, j] = signs[i + 1, j - 1] = signs[i + 1, j - 2] = false;
                    break;
                case 16:
                    signs[i, j] = signs[i, j + 1] = signs[i + 1, j + 1] = signs[i + 2, j + 1] = false;
                    break;
                case 26:
                    signs[i, j] = signs[i - 1, j] = signs[i - 1, j + 1] = signs[i - 1, j + 2] = false;              
                    break;
                case 36:
                    signs[i, j] = signs[i, j - 1] = signs[i - 1, j - 1] = signs[i - 2, j - 1] = false;
                    break;
                case 7:
                    signs[i, j] = signs[i, j - 1] = signs[i, j + 1] = signs[i + 1, j] = false;
                    break;
                case 17:
                    signs[i, j] = signs[i, j + 1] = signs[i - 1, j] = signs[i + 1, j] = false;
                    break;
                case 27:
                    signs[i, j] = signs[i, j - 1] = signs[i, j + 1] = signs[i - 1, j] = false;
                    break;
                case 37:
                    signs[i, j] = signs[i, j - 1] = signs[i + 1, j] = signs[i - 1, j] = false;
                    break;
            }
        }

        bool y_direction(uint type, uint i, uint j)//垂直方向 落地??障礙物??
        {
            switch (type)
            {
                case 1:
                    if (i != 0 && !signs[i-1, j]) return true;//i != 0 表示沒有碰到底部; !signs[i-1, j] 表示下一列、同行的位置沒有東西，若兩項條件達成，則可望下降，以下如上
                    else return false;

                case 11:
                    if (i != 0 && !signs[i - 1, j] && !signs[i - 1, j + 1] && !signs[i - 1, j + 2] && !signs[i - 1, j + 3]) return true;
                    else return false;

                case 2:
                    if (i != 0 && !signs[i-1, j] && !signs[i-1, j+1]) return true;
                    else return false;

                case 3:
                    if (i != 0 && !signs[i, j-1] && !signs[i-1, j] && !signs[i-1, j+1]) return true;
                    else return false;

                case 13:
                    if (i != 1 && !signs[i-2, j] && !signs[i-1, j+1])  return true;
                    else return false;

                case 4:
                    if (i != 0 && !signs[i, j+1] && !signs[i-1, j] && !signs[i-1, j-1]) return true;
                    else return false;

                case 14:
                    if (i != 1 && !signs[i-1, j] && !signs[i-2, j+1]) return true;
                    else return false;

                case 5:
                    if (i != 0 && !signs[i-1, j] && !signs[i, j+1] && !signs[i, j+2]) return true;
                    else return false;

                case 15:
                    if (i != 0 && !signs[i - 1, j] && !signs[i-1, j-1]) return true;
                    else return false;

                case 25:
                    if (i != 1 && !signs[i - 2, j] && !signs[i - 2, j - 1] && !signs[i - 2, j - 2]) return true;
                    else return false;

                case 35:
                    if (i != 2 && !signs[i - 1, j] && !signs[i - 3, j + 1]) return true;
                    else return false;

                case 6:
                    if (i != 0 && !signs[i, j-1] && !signs[i, j-2] && !signs[i-1, j]) return true;
                    else return false;

                case 16:
                    if (i != 0 && !signs[i-1, j] && !signs[i-1, j+1]) return true;
                    else return false;
                    
                case 26:
                    if (i != 1 && !signs[i-2, j] && !signs[i-2, j + 1] && !signs[i-2, j+2]) return true;
                    else return false;
                    
                case 36:
                    if (i != 2 && !signs[i-1, j] && !signs[i-3, j-1]) return true;
                    else return false;
                    
                case 7:
                    if (i != 0 && !signs[i-1, j-1] && !signs[i-1, j] && !signs[i-1, j+1]) return true;
                    else return false;
                    
                case 17:
                    if (i != 1 && !signs[i-2, j] && !signs[i - 1, j+1]) return true;
                    else return false;
                    
                case 27:
                    if (i != 1 && !signs[i - 1, j - 1] && !signs[i - 1, j + 1] && !signs[i - 2, j]) return true;
                    else return false;
                    
                case 37:
                    if (i != 1 && !signs[i-2, j] && !signs[i-1, j-1]) return true;
                    else return false;

                default:
                    return false;                  
            }
        }

        bool x_direction(uint type, uint i, uint j, int d)//水平方向 障礙物??
        { 
           switch(type)
           {
               case 1:
                   if (d == -1) //d = -1 則向左移動，以下如上
                    {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j - 1] && !signs[i + 2, j - 1] && !signs[i + 3, j - 1]) return true;
                        //j != 0 表示沒有碰到左側邊框，以下如上  
                        //!signs[i, j - 1] && !signs[i + 1, j - 1] && !signs[i + 2, j - 1] && !signs[i + 3, j - 1]  表示有空間向左移動，以下如上
                        else return false;
                   }
                    else //向右移動，以下如上
                    {
                       if (j != 9 && !signs[i, j + 1] && !signs[i + 1, j + 1] && !signs[i + 2, j + 1] && !signs[i + 3, j + 1]) return true;
                        //j != 9 表示沒有碰到右側邊框，以下如上
                        //!signs[i, j - 1] && !signs[i + 1, j - 1] && !signs[i + 2, j - 1] && !signs[i + 3, j - 1]  表示有空間向右移動，以下如上
                        else return false;
                   }

               case 11:
                   if (d == -1) 
                   {
                       if (j != 0 && !signs[i, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 6 && !signs[i, j + 4]) return true;
                       else return false;
                   }
               
               case 2:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 2]) return true;
                       else return false;
                   }

               case 3:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 1] && !signs[i + 1, j - 2]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 1]) return true;
                       else return false;
                   }

               case 13:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j] && !signs[i + 1, j-1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 2] && !signs[i - 1, j + 1]) return true;
                       else return false;
                   }

               case 4:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i + 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 1] && !signs[i + 1, j + 2]) return true;
                       else return false;
                   }
                  
               case 14:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j-1] && !signs[i+1, j-1] && !signs[i-1, j]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 1] && !signs[i-1, j + 2]) return true;
                       else return false;
                   }

               case 5:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 7 && !signs[i, j + 1] && !signs[i + 1, j + 3]) return true;
                       else return false;
                   }

               case 15:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i + 1, j - 2] && !signs[i + 2, j - 2]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 9 && !signs[i, j + 1] && !signs[i + 1, j] && !signs[i + 2, j]) return true;
                       else return false;
                   }
              
               case 25:
                   if (d == -1)
                   {
                       if (j != 2 && !signs[i, j-1] && !signs[i-1, j-3]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 9 && !signs[i, j + 1] && !signs[i-1, j+1]) return true;
                       else return false;
                   }
                
               case 35:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i-1, j] && !signs[i-2, j]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i-1, j+2] && !signs[i-2, j+2]) return true;
                       else return false;
                   }

               case 6:
                   if (d == -1)
                   {
                       if (j != 2 && !signs[i, j - 1] && !signs[i + 1, j - 3]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 9 && !signs[i, j + 1] && !signs[i + 1, j + 1]) return true;
                       else return false;
                   }

               case 16:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j] && !signs[i + 2, j]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 2] && !signs[i + 2, j + 2]) return true;
                       else return false;
                   }

               case 26:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i - 1, j-1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 7 && !signs[i, j + 1] && !signs[i - 1, j + 3]) return true;
                       else return false;
                   }

               case 36:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i - 1, j - 2] && !signs[i - 2, j - 2]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 9 && !signs[i, j + 1] && !signs[i - 1, j] && !signs[i - 2, j]) return true;
                       else return false;
                   }

               case 7:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i + 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 1]) return true;
                       else return false;
                   }

               case 17:
                   if (d == -1)
                   {
                       if (j != 0 && !signs[i, j - 1] && !signs[i + 1, j - 1] && !signs[i - 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i + 1, j + 1] && !signs[i - 1, j + 1]) return true;
                       else return false;
                   }

               case 27:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i - 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 8 && !signs[i, j + 2] && !signs[i - 1, j + 1]) return true;
                       else return false;
                   }
                   
               case 37:
                   if (d == -1)
                   {
                       if (j != 1 && !signs[i, j - 2] && !signs[i + 1, j - 1] && !signs[i - 1, j - 1]) return true;
                       else return false;
                   }
                   else
                   {
                       if (j != 9 && !signs[i, j + 1] && !signs[i + 1, j + 1] && !signs[i - 1, j + 1]) return true;
                       else return false;
                   }

               default:
                   return false;
           }            
        }
        
        void show_grids()//降下的積木
        {
            int i, j;
            for (i = 0; i < 20; i++)
                for (j = 0; j < 10; j++)
                    if (signs[i, j])
                        grids[i, j].BackColor = grids_color[i, j];//若新的位置有積木，則顯示其顏色
                    else
                        grids[i, j].BackColor = Color.Black;//其餘顯示黑色
        }

        void display_next_block(uint type)//提示下一個積木
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                    next[i, j].BackColor = Color.White;

                switch (type)
                {
                    case 1:
                        next[0, 1].BackColor = next[1, 1].BackColor = next[2, 1].BackColor = next[3, 1].BackColor = Color.Blue;                        
                        break;
                    case 2:
                        next[1, 0].BackColor = next[1, 1].BackColor = next[2, 0].BackColor = next[2, 1].BackColor = Color.Yellow;                     
                        break;
                    case 3:
                        next[2, 0].BackColor = next[2, 1].BackColor = next[1, 1].BackColor = next[1, 2].BackColor = Color.Red; 
                        break;
                    case 4:
                        next[1, 0].BackColor = next[1, 1].BackColor = next[2, 1].BackColor = next[2, 2].BackColor = Color.Green; 
                        break;
                    case 5:
                        next[1, 0].BackColor = next[2, 0].BackColor = next[2, 1].BackColor = next[2, 2].BackColor = Color.Orange;
                        break;
                    case 6:
                        next[2, 0].BackColor = next[2, 1].BackColor = next[2, 2].BackColor = next[1, 2].BackColor = Color.LightBlue;
                        break;
                    case 7:
                       next[1, 0].BackColor = next[1, 1].BackColor = next[1, 2].BackColor = next[2, 1].BackColor = Color.Purple;
                       break;
                }
        }
        
        private void timer1_Tick(object sender, EventArgs e)//積木落下機制
        {
            if (y_direction(block_type, block_row, block_col))//偵測可不可以下降，若可以就更新位置
            {
                block_row_pre = block_row; block_row_pre = block_row; block_type_pre = block_type;
                block_row--;

                if (block_row == 19)//積木已下降
                {
                    block_type_next = (uint)rander.Next(0, 7) + 1;//產生下一個積木
                    display_next_block(block_type_next);//顯示下一個積木
                    block_count++;//積木總數+1
                    score += 5;//分數加5
                    label_block_count.Text = "Blocks:" + block_count.ToString();//將block_count的字元轉字串
                    label_score.Text = "Score:" + score.ToString();//將score的字元轉字串
                    if (game_mode == 1)
                    {
                        timer_interval = 1010 - (int)(score / 150) * 50;
                        if (timer_interval <= 0)
                            timer_interval = 10;//減少降落的時間

                        timer1.Interval = timer_interval;
                        label_level.Text = "Level:" + ((1010 - timer_interval) / 50).ToString();//將((1010 - timer_interval) / 50)的字元轉字串
                    }                    
                  }
                erase_block(block_row_pre, block_col_pre, block_type_pre);//清除之前的位置
                update_block(block_row, block_col, block_type);//更新位置
                show_grids();//顯現位置
                block_row_pre = block_row;
                block_changed = false;
            }
            else 
            {             
                show_grids();
                full_line_check();
                if (block_row == 20)
                {                    
                  label_info.Text = "Game Over!";
                  System.Media.SoundPlayer player = new System.Media.SoundPlayer();//音效
                  player.SoundLocation = "xblocks.wav";
                  player.Stop();
                  timer1.Enabled = false;//積木落下到計時關閉
                  return;

                }
                block_type = block_type_next;//初始化 
                block_row = 20;
                block_col = 4;
                block_row_pre = 20;
                block_col_pre = 4;
                block_type_pre = block_type;
                block_changed = false;
            }
        }

        void full_line_check()//刪除填滿的列
        {
            uint row_sum;
            uint i, j;

            i = 0;
            while(i < 20)//遊戲尚未結束
            {
                row_sum = 0;
                for (j = 0; j < 10; j++)
                    if (signs[i, j]) row_sum++;

                if (row_sum == 10)//集滿一列
                {
                    score += 20;//分數+20
                    label_score.Text = "Score:" + score.ToString();//將score的字元轉字串
                    for (j = 0; j < 10; j++)
                        signs[i, j] = false;

                    show_grids(); 

                    for (uint y = i; y < 21; y++)//將每列下降一列
                        for (j = 0; j < 10; j++)
                        {
                            signs[y, j] = signs[y + 1, j];
                            grids_color[y, j] = grids_color[y + 1, j];
                        }
                    show_grids();
                }
                else i++;
            }
        }

        uint next_block_type(uint type, uint i, uint j)//方塊翻轉
        {
            switch(type)
            {
                case 1:
                    if (j <= 7 && j>=1 && !signs[i+2, j-1] && !signs[i+2, j + 1] && !signs[i+2, j + 2])
                    //若有空間讓旋轉支點做旋轉，則可以旋轉，以下如上
                    {
                        block_row = i + 2; block_col = j - 1;
                        return 11;//呼叫旋轉後的圖示，以下如上
                    }
                    else return 1;
                
                case 11:
                    if (i >= 2 && !signs[i - 1, j+1] && !signs[i - 2, j+1] && !signs[i +1, j+1])
                    {
                        block_row = i -2; block_col = j + 1;
                        return 1;
                    }
                    else return 11; 
                
                case 2: return 2; 
                
                case 3:
                    if (i >= 1 && !signs[i + 1, j+1] && !signs[i-1, j])
                        return 13;
                    else return 3;  

                case 13:
                    if (j >= 1 && !signs[i + 1, j] && !signs[i+1, j-1])
                        return 3;
                    else return 13;  
                
                case 4:
                    if (i >= 1 && !signs[i, j+1] && !signs[i-1, j+1])
                        return 14;
                    else return 4;  

                case 14:
                    if (j >= 1 && !signs[i, j-1] && !signs[i + 1, j + 1])
                        return 4;
                    else return 14;   

                case 5:
                    if (!signs[i + 2, j] && !signs[i, j + 1])
                    {
                        block_col = j + 1;
                        return 15;
                    }
                    else return 5;   

                case 15:
                    if (j >= 2 && !signs[i, j-2] && !signs[i + 1, j] )
                    {
                        block_row = i + 1;
                        return 25;
                    }
                    else return 15;                       

                case 25:
                    if (i >= 2 && !signs[i, j - 1] && !signs[i - 2, j])
                    {
                        block_col = j - 1;
                        return 35;
                    }
                    else return 25;  

                case 35:
                    if (j <= 7 && !signs[i - 1, j] && !signs[i, j + 2])
                    {
                        block_row = i - 1;
                        return 5;
                    }
                    else return 35; 

                case 6:
                    if (!signs[i, j - 1] && !signs[i + 2, j] )
                    {
                        block_col = j - 1;
                        return 16;
                    }
                    else return 6; 

                case 16:
                    if (j <= 7 && !signs[i - 1, j] && !signs[i, j + 2])
                    {
                        block_row = i + 1;
                        return 26;
                    }
                    else return 16; 

                case 26:
                    if (i >= 2 && !signs[i, j +1] && !signs[i - 2, j])
                    {
                        block_col = j + 1;
                        return 36;
                    }
                    else return 26; 

                case 36:
                    if (j >= 2 && !signs[i, j-2] && !signs[i - 1, j])
                    {
                        block_row = i - 1;
                        return 6;
                    }
                    else return 36; 

                case 7:
                    if (i>=1 && !signs[i-1, j])
                        return 17;
                    else return 7;  

                case 17:
                    if (j >= 1 && !signs[i, j-1])
                        return 27;
                    else return 17;  

                case 27:
                    if (!signs[i+1, j])
                        return 37;
                    else return 27;  

                case 37:
                    if (j<=8 && !signs[i, j+1])
                        return 7;
                    else return 37;  

                default: return 0;  
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//偵測按下鍵盤
        {
            if (e.KeyCode == Keys.P)//遊戲暫停
            {
                if (game_mode == 0) { game_mode = 1; timer1.Enabled = true; }
                else { game_mode = 0; timer1.Enabled = false; }
            }

            if (e.KeyCode == Keys.Left)//向左移動
            {
                if (x_direction(block_type, block_row, block_col, -1))
                {
                    block_col_pre = block_col; block_col--;
                    block_changed = true;
                }
            }

            if (e.KeyCode == Keys.Right)//向右移動
            {
                if (x_direction(block_type, block_row, block_col, 1))
                {
                    block_col_pre = block_col; block_col++;
                    block_changed = true;
                }
            }

            if (e.KeyCode == Keys.Space)//積木變形
            {
                block_type_pre = block_type;
                block_col_pre = block_col; block_row_pre = block_row;
                block_type = next_block_type(block_type, block_row, block_col);
                if(block_type != block_type_pre)
                    block_changed = true;
            }

            if (e.KeyCode == Keys.S)//減少積木下降的時間
            {
                game_mode = 2;               
                timer_interval -= 50;
               
                if(timer_interval <= 0)
                    timer_interval = 1;
                
                timer1.Interval = timer_interval;
                label_level.Text = "Level:" + ((1010 - timer_interval) / 50).ToString();
            }

            if (e.KeyCode == Keys.A)//增加積木下降的時間
            {
                game_mode = 2;
                timer_interval += 50;

                if (timer_interval >= 1010)
                    timer_interval = 1010;

                timer1.Interval = timer_interval;
                label_level.Text = "Level:" + (1010 - timer_interval) / 50;
            }

            if (e.KeyCode == Keys.Down)//積木下移
                timer1.Interval = 15;
      
            if (block_changed)//如果積木變形
            {
                erase_block(block_row_pre, block_col_pre, block_type_pre);//消除之前的的積木型態
                update_block(block_row, block_col, block_type);//更新新的積木型態
                show_grids();
                block_row_pre = block_row; block_col_pre = block_col; block_type_pre = block_type;
                block_changed = false;
            }
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)//多按的
        {
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)//偵測放開鍵盤
        {
            if (e.KeyCode == Keys.Down)
            {
                timer1.Interval = timer_interval;//如果按下向下鍵，改變向下的速度
            }
        }

        private void label_info_Click(object sender, EventArgs e)//多按的
        {

        }

        private void button2_Click(object sender, EventArgs e)//多按的
        {

        }

        private void button4_Click(object sender, EventArgs e)//多按的
        {

        }

        private void button3_Click(object sender, EventArgs e)//多按的
        {

        }

        private void button5_Click(object sender, EventArgs e)//遊戲暫停
        {
            if (game_mode == 0) { game_mode = 1; timer1.Enabled = true; }
            else { game_mode = 0; timer1.Enabled = false; }
        }

        private void button6_Click(object sender, EventArgs e)//增加level
        {
            game_mode = 2;
            timer_interval -= 50;

            if (timer_interval <= 0)
                timer_interval = 1;

            timer1.Interval = timer_interval;
            label_level.Text = "Level:" + ((1010 - timer_interval) / 50).ToString();
        }

        private void button7_Click(object sender, EventArgs e)//減少level
        {
            game_mode = 2;
            timer_interval += 50;

            if (timer_interval >= 1010)
                timer_interval = 1010;

            timer1.Interval = timer_interval;
            label_level.Text = "Level:" + (1010 - timer_interval) / 50;
        }

        private void Form1_Load(object sender, EventArgs e)//button、KeyPreview設定
        {
            this.KeyPreview = true;
            btn btn5 = new btn();
            this.Controls.Add(btn5);
        }
        private class btn : Button//不要讓button 與 keydown 衝突
        {
            protected override bool IsInputKey(Keys keyData)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Down
                    || keyData == Keys.P || keyData == Keys.S || keyData == Keys.A)
                { return true; }
                else
                {
                    return base.IsInputKey(keyData);
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)//新的一局
        {
            MessageBox.Show("新的一局");
            init_game();
            label_info.Text = "";  //game over 消失         
            timer1.Enabled = true;//開始遊戲
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();//音效
            player.SoundLocation = "xblocks.wav";
            player.Load();
            player.PlayLooping();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
        }
    }
}
