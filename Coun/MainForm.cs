/*
 * Created by SharpDevelop.
 * User: Plyskay
 * Date: 9/5/2018
 * Time: 10:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace Coun
{
	public partial class MainForm : Form
	{
	  //const	 int plan = 1725;
	  //const int d = 432;
	  TcpClient clientSocket = new TcpClient();
		public MainForm()
		{
			
			InitializeComponent();
			
			
		
			    int y;
			    int m = 432;
				int z =Co(out y, out m);
				label5.Text=z.ToString();
				label6.Text=y.ToString();
              // label1.Text = DateTime.Now.ToString("h:mm tt");
               label7.Text=m.ToString();
             
			
		}
		
		public int Co(out int y, out int m)
		{
			   var d1 = DateTime.Now;
			   DateTime d2 = new DateTime();
				int x = 0;
				 y = 0;
				 m = 432;
			try{
				//Thread.Sleep(60000);	
				//string[] filePaths = Directory.GetFiles(@"L:\DATA\Aero3_EOL\reports","*.txt");
				string[] filePaths = Directory.GetFiles(@"C:\Users\plyskay\Desktop\11","*.txt");
				
				foreach(var st in filePaths)
				{
				 string filebody = File.ReadAllText(st);
				 d2 = File.GetLastWriteTime(st);
				 if((filebody.Contains("UUT Result: Passed"))&((d1.DayOfYear == d2.DayOfYear)||((int)d1.DayOfYear-(int)d2.DayOfYear==1 )))
				{
				 	if((d2.Hour >=6)&&((float)d2.Hour<=18.3)&&(d1.DayOfYear == d2.DayOfYear)){
				 		x++; m--;}
				 	else if(((float)d2.Hour >18.3)&&(d2.Hour<=23)&&(d1.DayOfYear == d2.DayOfYear)){
				 		x++; m--;
				 	}else if(((d2.Hour >=0) && (d2.Hour <=6)&& (d1.DayOfYear == d2.DayOfYear))){
				 		x++; m--;
				 	}
				 	
				}
				 if((filebody.Contains("UUT Result: Failed"))&&((d1.DayOfYear == d2.DayOfYear)||((int)d1.DayOfYear-(int)d2.DayOfYear==1 )))
				 {
				 	if((d2.Hour >=6)&((float)d2.Hour<=18.3)&(d1.DayOfYear == d2.DayOfYear)){
				 		y++;}
				 	else if(((float)d2.Hour >18.3)&(d2.Hour<=23)&(d1.DayOfYear == d2.DayOfYear)){
				 		y++;
				 	}else if(((d2.Hour >=0) & (d2.Hour <=6)& (d1.DayOfYear == d2.DayOfYear))){
				 		y++;
				   }
				}
				
				}			
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			if(((d1.Hour== 6)&&(d1.Minute <2))|((d1.Hour == 18) &&(d1.Minute < 32) ))
			   {
			    
			   	x = 0;
			   	y = 0;
			   	m = 432;
			   }
		//C:\Users\plyskay\Documents
		//C:\TEMP\16\Data
		try{
			File.WriteAllLines(@"C:\Users\plyskay\Documents\Data.txt", new string [] {x.ToString(),y.ToString()});
			}
		catch(Exception e){
			MessageBox.Show(e.ToString());
		}
			return x;		
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			//TimeSpan sp;
			//string d = DateTime.Now.ToString("h:mm tt");
			//label1.Text = d;
			//var s1= DateTime.Now;
		    //Thread.Sleep(1000);
			//var s2 = DateTime.Now.AddSeconds(10.0d);
			
			//sp = s2-s1;
			//double r = sp.TotalSeconds;
		    //if(r>=10)
			
				int y;
				int m = 432;
				int z = Co(out y, out m);
				label5.Text=z.ToString();
				label6.Text=y.ToString();
				label7.Text=m.ToString();
				string data = z + ","+ y;
			NetworkStream serverStream = clientSocket.GetStream();
			byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

//            byte[] inStream = new byte[10025];
//            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
//            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
		    //	}
	
		}
	
		void Label2Click(object sender, EventArgs e)
		{
	
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			try{
				clientSocket.Connect("10.73.88.147", 8888);}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			
			}
		
		}
			
			//Refresh();
		}
		
		
	}

