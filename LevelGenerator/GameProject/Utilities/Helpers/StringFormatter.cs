using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LevelGenerator.GameProject.Utilities.Helpers
{
    public static class StringFormatter
    {
        public static string CheckProjectName(string projName, string projPath)
        {
            List<string> dirs = Directory.GetDirectories(projPath).ToList();
            List<int> nums = new List<int>();
            string currentProjName = "";
            string newProjName = "";
            int futureProjNum = 0;
            int highestProjNum = 0;
            bool projNeedsIncrement = false;
            string projNum = new string(projName.Where(char.IsDigit).ToArray());
            string trimmedProjName = Regex.Replace(projName, $"{projNum}$", "");
            if (Directory.Exists(projPath + $@"\\{projName}"))
            {
                foreach (var d in dirs)
                {
                    if (d.ToLower().Contains(trimmedProjName.ToLower()))
                    {
                        projNeedsIncrement = true;
                        currentProjName = d.ToLower();
                        string loweredName = trimmedProjName.ToLower();
                        int dInt = currentProjName.IndexOf(loweredName);
                        currentProjName = currentProjName.Substring(dInt);
                        string digits = new string(currentProjName.Where(char.IsDigit).ToArray());
                        string letters = new string(currentProjName.Where(char.IsLetter).ToArray());
                        if (int.TryParse(digits, out int realNum))
                        {
                            nums.Add(realNum);
                            if (realNum > highestProjNum)
                            {
                                highestProjNum = realNum;
                            }
                        }
                    }
                }

                if (projNeedsIncrement)
                {
                    nums.Sort();
                    if (nums.Count > 0)
                    {
                        foreach (int d in nums)
                        {
                            int totalNums = nums.Count - 1;
                            int currentIndex = nums.IndexOf(d);
                            if (totalNums != currentIndex)
                            {
                                int numIndex = nums.IndexOf(d) + 1;
                                int nextNum = nums[numIndex];
                                if (d + 1 != nextNum)
                                {
                                    futureProjNum = d + 1;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        futureProjNum++;
                    }

                    string n = new string(projName.Where(char.IsDigit).ToArray());
                    if (n != "")
                    {
                        newProjName = projName.Remove(projName.IndexOf(n.ToString()));
                    }
                    else
                    {
                        newProjName = projName;
                    }


                    if (int.TryParse(projNum, out int actualNum) && actualNum > highestProjNum)
                    {
                        newProjName = newProjName + actualNum.ToString();
                    }
                    else
                    {
                        newProjName = newProjName + futureProjNum.ToString();
                    }

                }

            }
            else
            {
                return projName;
            }
            return newProjName;
        }
    }
}
