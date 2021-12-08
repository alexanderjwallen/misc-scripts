/*

This was made because many games, mostly unity, store their save data in the "binaryformatter" format.
It let's you drag a dat file on top of it(or provide it as an argument) and it will convert the data to xml
then if you want you can drag the xml file it creates back on it, and generate a new dat file.

*/




using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace BinaryXML
{
	class BinaryXML 
	{
		static int Main(string[] args) 
		{

			if (args.Length == 1 && File.Exists(args[0])) {
				string path = args[0];
				string fileName = Path.GetFileName(path);
				string fileExtention = Path.GetExtension(path);

				if (fileExtention.Equals(".xml")) {
					path = Path.ChangeExtension(path,"dat");
					string xmlText = System.IO.File.ReadAllText(path);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					FileStream output = File.Create(path);
					binaryFormatter.Serialize(output,xmlText);
					output.Close();
					return 0;
				} else {
					string xmlOut;
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					FileStream binaryFileIn = File.Open(path,FileMode.Open);
					try {
						xmlOut = (string)binaryFormatter.Deserialize(binaryFileIn);
					}
					catch (System.Runtime.Serialization.SerializationException) {
						Console.WriteLine("That's probably not a BinaryFormatter file.");
						return 1;
					}
					binaryFileIn.Close();
					path = Path.ChangeExtension(path,"xml");
					System.IO.File.WriteAllText(@path,xmlOut);
					return 0;
				}
			} else {
				Console.WriteLine("Usage: binaryxml.exe <BinaryFormatter File>.dat\nbinaryxml.exe <XML File>.xml");
				return 1;
			}
		}
	}
}
