using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blockchain
{
    public class Blockchain  
{  

    public IList<Block> Chain { get;  set; }  

    
  
    public Blockchain()  
    {  
        InitializeChain();  
        AddGenesisBlock(); 
        GetCurrentChain();
        
    }  

    public IList GetCurrentChain()
    {
        var fileReader = new System.IO.StreamReader("Blockchain.txt");
        List<string> stringsFromFile = new List<string>();
        string lineOfText;
        while ((lineOfText = fileReader.ReadLine()) != null)
        {
            stringsFromFile.Add(lineOfText);
        }
        Console.WriteLine(stringsFromFile[4].Contains("TimeStamp"));
        Console.WriteLine("done");
        return stringsFromFile;
    }
  
  
    public void InitializeChain()  
    {  
        Chain = new List<Block>();  
    }  
  
    public Block CreateGenesisBlock()  
    {  
        return new Block( DateTime.Now, null, "{}");  
    }  
  
    public void AddGenesisBlock()  
    {  
        Chain.Add(CreateGenesisBlock());  
    }  
      
    public Block GetLatestBlock()  
    {  
        return Chain[Chain.Count - 1];  
    }  
  
    public void AddBlock(Block block)  
    {  
        Block latestBlock = GetLatestBlock();  
        block.Index = latestBlock.Index + 1;  
        block.PreviousHash = latestBlock.Hash;  
        block.Hash = block.CalculateHash();  
        Chain.Add(block);  
    } 

    public bool IsValid()  
{  
    for (int i = 1; i < Chain.Count; i++)  
    {  
        Block currentBlock = Chain[i];  
        Block previousBlock = Chain[i - 1];  
  
        if (currentBlock.Hash != currentBlock.CalculateHash())  
        {  
            return false;  
        }  
  
        if (currentBlock.PreviousHash != previousBlock.Hash)  
        {  
            return false;  
        }  
    }  
    return true;  
} 

} 
}