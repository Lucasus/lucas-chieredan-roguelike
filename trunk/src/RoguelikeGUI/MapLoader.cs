using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace RoguelikeGUI
{
	public class MapLoader
	{
		public char[,] loadMap(string mapFileName)
		{
			StreamReader reader = new StreamReader(@"Maps/"+mapFileName);
			List<string> lines = new List<string>();
			while(!reader.EndOfStream)
			{
				lines.Add(reader.ReadLine());
			}
			char[,] mapText = new char[lines.Count, lines[0].Length];
			for(int j=0; j<lines.Count; j++)
			{
				for(int i=0; i<lines[j].Length; i++)
				{
					mapText[j,i] = lines[j][i];
				}
			}
			return mapText;
		}
	}
}
