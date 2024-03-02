using SharpFileServiceProg.Service;
namespace SharpFileServiceProg.Operations.Index
{
    internal class IndexOperations : IFileService.IIndexWrk
    {
        public int GetLocaLast(string loca)
        {
            var tmp = loca.Split("/");
            var lastString = tmp.Last();
            var last = StringToIndex(lastString);
            return last;
        }

        public (string, string) JoinIndexWithLoca(
            (string Repo, string Loca) adrTuple, int? index)
        {
            if (index == null)
            {
                return adrTuple;
            }

            var idxString = IndexToString(index);

            var newLoca = JoinLoca(adrTuple.Loca, idxString);
            var newAdrTuple = (adrTuple.Repo, newLoca);
            return newAdrTuple;
        }

        public (string, string) AdrTupleJoinLoca(
            (string Repo, string Loca) adrTuple, string loca)
        {
            if (loca == string.Empty)
            {
                return adrTuple;
            }

            var newLoca = JoinLoca(adrTuple.Loca, loca);
            var newAdrTuple = (adrTuple.Repo, newLoca);
            return newAdrTuple;
        }

        public string JoinLoca(string loca01, string loca02)
        {
            if (loca01 == string.Empty)
            {
                return loca02;
            }

            var newLoca = loca01 + "/" + loca02;
            return newLoca;
        }


        public string IndexToString(int? index)
        {
            if (index < 10)
            {
                return "0" + index;
            }
            if (index < 100)
            {
                return index.ToString();
            }
            if (index < 1000)
            {
                return index.ToString();
            }

            throw new Exception();
        }

        public int StringToIndex(string input)
        {
            if (input.Length > 3)
            {
                throw new Exception();
            }

            var index = int.Parse(input);
            return index;
        }

        public int TryStringToIndex(string input)
        {
            try
            {
                var index = StringToIndex(input);
                return index;
            }
            catch
            {
                return -1;
            }
        }

        public string LastTwoChar(string input)
        {
            var lastTwo = input.Substring(input.Length - 2, 2);
            return lastTwo;
        }

        public bool IsCorrectIndex(string input)
        {
            var lastTwo = LastTwoChar(input);
            var index = TryStringToIndex(lastTwo);
            if (index == -1)
            {
                return false;
            }

            return true;
        }

        public bool IsCorrectIndex(string input, out int index)
        {
            var lastTwo = LastTwoChar(input);
            index = TryStringToIndex(lastTwo);
            if (index == -1)
            {
                return false;
            }

            return true;
        }
    }
}
