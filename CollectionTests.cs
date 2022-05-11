using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.Unit.Tests
{
    public class CollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            Assert.That(nums.Count == 0, "Count property");
            //Assert.That(nums.Capacity == 0, "Capacity property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_Constructor_SingleItem()
        {
            //Arrange
            var nums = new Collection<int>(5);

            //Act
            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5]");
        }

        [Test]
        public void Test_Collection_Constructor_MultipleItem()
        {
            //Arrange
            var nums = new Collection<int>(5, 6);

            //Act
            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5, 6]");
        }

        [Test]
        public void Test_Collection_Constructor_MultipleItemString()
        {
            //Arrange
            var nums = new Collection<string>("AG", "AG");

            //Act
            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[AG, AG]");
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            nums.Add(7);

            //Assert
            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[7]");
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            //Arrange
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            //Act
            nums.AddRange(items);

            //Assert
            Assert.That(nums.Count == 3, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[6, 7, 8]");
        }

        [Test]
        [Timeout(5000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000;
            var nums = new Collection<int>();

            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Ani", "Viki");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("Ani"));
            Assert.That(item1, Is.EqualTo("Viki"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Ani", "Viki");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Ani, Viki]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Ani", "Viki");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Ani, Viki], [10, 20], []]"));
        }

    }
}