﻿using System;
using System.Collections.Generic;
using System.IO;
using AgentCsvGenerator.Config;
using AgentCsvGenerator.Generators;

namespace AgentCsvGenerator
{
//    http://www.hamstermap.com/quickmap.php
//    -24.8690,31.1944
//    -24.8239,31.2436
    internal static class AgentCsvGenerator
    {
        private static void Main()
        {
            BushbuckridgeSite();
            SkukuzaSite();

            Console.WriteLine("Files are generated :)");
        }

        private static void BushbuckridgeSite()
        {
            var area = new AreaDefinition
            ( // 5km x 5km (2500ha)
                west: 31.194,
                east: 31.244,
                north: -24.824,
                south: -24.870,
                widthInMeter: 5000,
                lengthInMeter: 5000
            );

            var households = new HouseholdGenerator(area).Generate(684);
            SaveContentInFile(Path.Combine("..", "..", "model_input", "household.csv"), households);

            var treeTypes = new List<TreeType>();
            treeTypes.Add(new TreeType("sb", 31, 73, 7));
            treeTypes.Add(new TreeType("ca", 31, 131, 3));
            treeTypes.Add(new TreeType("an", 8, 2, 0));
            treeTypes.Add(new TreeType("tt", 3546, 638, 38));

            var trees = new TreeGenerator(area).Generate(treeTypes);
            SaveContentInFile(Path.Combine("..", "..", "model_input", "tree_bushbuckridge.csv"), trees);
        }

        private static void SkukuzaSite()
        {
            var area = new AreaDefinition
            ( // 4km x 4km (1600ha)
                west: 31.472,
                east: 31.521,
                north: -24.986,
                south: -25.031,
                widthInMeter: 4000,
                lengthInMeter: 4000
            );

            var treeTypes = new List<TreeType>();
            treeTypes.Add(new TreeType("sb", 89, 17, 1));
            treeTypes.Add(new TreeType("ca", 2888, 550, 26));
            treeTypes.Add(new TreeType("an", 683, 130, 7));
            treeTypes.Add(new TreeType("tt", 1817, 300, 46));

            var trees = new TreeGenerator(area).Generate(treeTypes);
            SaveContentInFile(Path.Combine("..", "..", "model_input", "tree_skukuza.csv"), trees);
        }

        private static void SaveContentInFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}