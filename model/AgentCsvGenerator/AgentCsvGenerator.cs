﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using AgentCsvGenerator.Config;
using AgentCsvGenerator.Generators;

namespace AgentCsvGenerator
{
//    http://www.hamstermap.com/quickmap.php
//    -24.8690,31.1944
//    -24.8239,31.2436
    internal static class AgentCsvGenerator
    {
        public const string Delimiter = ";";

        private static void Main()
        {
            BushbuckridgeSite();
//            SkukuzaSite();

            Console.WriteLine("Files are generated :)");
        }

        private static void BushbuckridgeSite()
        {
            var area = new AreaDefinition
            ( // 5km x 5km (2500ha)
                west: 31.194250,
                east: 31.2430319,
                north: -24.823869,
                south: -24.8695669,
                widthInMeter: 5000,
                lengthInMeter: 5000
            );

            var households = new HouseholdGenerator(area).Generate(684);
//            SaveContentInFile(Path.Combine("..", "..", "model_input", "household.csv"), households);

            var species = new List<Species>();
            species.Add(new Species("sb", 31, 73, 7, 20));
            species.Add(new Species("ca", 31, 131, 3, 10));
            species.Add(new Species("an", 8, 2, 0, 8));
            species.Add(new Species("tt", 3546, 638, 38, 13));

            var trees = new TreeGenerator(area).Generate(species, IsEmptyRaster, EvaluateFilterPercentage);
            SaveContentInFile(Path.Combine("..", "..", "model_input", "tree_bushbuckridge_10x10.csv"), trees);

            var raster = new TreeRasterGenerator(area).Generate(IsEmptyRaster);
            var filePath = Path.Combine("..", "..", "model_input", "tree_bushbuckridge_raster_10x10.asc");
            SaveContentInZip(filePath, raster);
        }


        private static bool IsEmptyRaster(int rasterLatIndex, int rasterLonIndex)
        {
//            if (rasterLonIndex < 10 && (rasterLatIndex < 20 || rasterLatIndex >= 30))
//            {
//                return true;
//            }
//
//            if (rasterLonIndex >= 10 && rasterLonIndex < 20 && rasterLatIndex >= 20 && rasterLatIndex < 30)
//            {
//                return true;
//            }
//
//            if (rasterLonIndex >= 30 && rasterLonIndex < 40 && rasterLatIndex >= 30 && rasterLatIndex < 40)
//            {
//                return true;
//            }
//
//            if (rasterLonIndex >= 40 && rasterLatIndex >= 20)
//            {
//                return true;
//            }

            if (rasterLatIndex >= 20 && rasterLatIndex < 30 && rasterLonIndex >= 20 && rasterLonIndex < 30)
            {
                return false;
            }

            return true;
        }

        private static double EvaluateFilterPercentage(int rasterLatIndex, int rasterLonIndex)
        {
//            if (rasterLatIndex < 15)
//            {
//                return 0.2;
//            }

            return 1;
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

            var species = new List<Species>();
            species.Add(new Species("sb", 89, 17, 1, 20));
            species.Add(new Species("ca", 2888, 550, 26, 10));
            species.Add(new Species("an", 683, 130, 7, 8));
            species.Add(new Species("tt", 1817, 300, 46, 13));

            var trees = new TreeGenerator(area).Generate(species, IsEmptyRaster);
            SaveContentInFile(Path.Combine("..", "..", "model_input", "tree_skukuza_20x29.csv"), trees);


            var raster = new TreeRasterGenerator(area).Generate(IsEmptyRaster);
            var filePath = Path.Combine("..", "..", "model_input", "tree_skukuza_raster_20x29.asc");
            SaveContentInZip(filePath, raster);
        }

        private static void SaveContentInFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        private static void SaveContentInZip(string filePath, string content)
        {
            var zipPath = Path.Combine(Path.GetDirectoryName(filePath),
                Path.GetFileNameWithoutExtension(filePath) + ".zip");
            File.WriteAllText(filePath, content);

            File.Delete(zipPath);
            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                archive.Dispose();
            }

            File.Delete(filePath);
        }
    }
}