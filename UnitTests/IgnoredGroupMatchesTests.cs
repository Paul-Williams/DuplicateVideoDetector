//#nullable enable

using System;
using System.Collections.Generic;
using DuplicateVidDetector.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PW.IO.FileSystemObjects;

namespace UnitTests
{
  [TestClass]
  public class IgnoredGroupMatchesTests
  {
    [TestMethod]
    public void ExactSamePathsTest()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsTrue(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void ExactSamePathsDifferentOrderTest()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file2.txt", (FilePath)@"C:\file1.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsTrue(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void SamePathsDifferenctCaseTest()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\File1.txt", (FilePath)@"C:\File2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsTrue(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void OneDifferentPathTest()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file11.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void AllDifferentPathTest()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file11.txt", (FilePath)@"C:\file22.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void SamePathsButExtraTestL()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" , (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void SamePathsButExtraTestR()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void ExtraTestL()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt", (FilePath)@"C:\file3.txt" };
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void ExtraTestR()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt", (FilePath)@"C:\file3.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void NullTestL()
    {
      var pathsR = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup();
      var groupR = new IgnoredGroup() { Files = pathsR };

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }

    [TestMethod]
    public void NullTestR()
    {
      var pathsL = new[] { (FilePath)@"C:\file1.txt", (FilePath)@"C:\file2.txt" };

      var groupL = new IgnoredGroup() { Files = pathsL };
      var groupR = new IgnoredGroup();

      Assert.IsFalse(groupL.Matches(groupR.Files));

    }



  }
}
