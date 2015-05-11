﻿using RimDev.Filter.Generic;
using RimDev.Filter.Range;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RimDev.Filter.Tests.Generic
{
    public class IEnumerable_1ExtensionsTests
    {
        public class Filter : IEnumerable_1ExtensionsTests
        {
            private class Person
            {
                public char FavoriteLetter { get; set; }
                public int FavoriteNumber { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }

            private readonly IEnumerable<Person> people = new List<Person>()
            {
                new Person()
                {
                    FavoriteLetter = 'a',
                    FavoriteNumber = 5,
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Person()
                {
                    FavoriteLetter = 'b',
                    FavoriteNumber = 10,
                    FirstName = "Tim",
                    LastName = "Smith"
                },
            };

            public class ConstantFilters : Filter
            {
                [Fact]
                public void Should_filter_when_property_types_match_as_constant_string()
                {
                    var @return = people.Filter(new
                    {
                        FirstName = "John"
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_constant_integer()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = 5
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }
            }

            public class EnumerableFilters : Filter
            {
                [Fact]
                public void Should_bypass_filter_when_empty_collection()
                {
                    var @return = people.Filter(new
                    {
                        FirstName = new string[] { }
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(2, @return.Count());
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_enumerable_string()
                {
                    var @return = people.Filter(new
                    {
                        FirstName = new[] { "John" }
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_enumerable_integer()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = new[] { 5 }
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }
            }

            public class RangeFilters : Filter
            {
                [Theory,
                InlineData("(,5]"),
                InlineData("(-∞,5]")]
                public void Should_filter_open_ended_lower_bound(string value)
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<int>(value)
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Theory,
                InlineData("(5,]"),
                InlineData("(5,+∞)")]
                public void Should_filter_open_ended_upper_bound(string value)
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<int>(value)
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("Tim", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_for_concrete_range()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = new RimDev.Filter.Range.Generic.Range<int>()
                        {
                            MinValue = 5,
                            MaxValue = 5,
                            IsMinInclusive = true,
                            IsMaxInclusive = true
                        }
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_byte()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<byte>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_char()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteLetter = RimDev.Filter.Range.Range.FromString<char>("[a,b)")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_decimal()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<decimal>("[4.5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_double()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<double>("[4.5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_float()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<float>("[4.5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_int()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<int>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_long()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<long>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_sbyte()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<sbyte>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_short()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<short>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_uint()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<uint>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_ulong()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<ulong>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }

                [Fact]
                public void Should_filter_when_property_types_match_as_range_ushort()
                {
                    var @return = people.Filter(new
                    {
                        FavoriteNumber = RimDev.Filter.Range.Range.FromString<ushort>("[5,5]")
                    });

                    Assert.NotNull(@return);
                    Assert.Equal(1, @return.Count());
                    Assert.Equal("John", @return.First().FirstName);
                }
            }

            [Fact]
            public void Should_filter_multiple_properties()
            {
                var @return = people.Filter(new
                {
                    FirstName = "John",
                    FavoriteNumber = 0
                });

                Assert.NotNull(@return);
                Assert.Equal(0, @return.Count());
            }

            [Fact]
            public void Should_not_throw_if_filter_does_not_contain_valid_properties()
            {
                var @return = people.Filter(new
                {
                    DOESNOTEXIST = ""
                });

                Assert.NotNull(@return);
                Assert.Equal(2, @return.Count());
            }

            [Fact]
            public void Should_not_throw_if_filter_constant_type_does_not_match()
            {
                var @return = people.Filter(new
                {
                    FirstName = 1
                });

                Assert.NotNull(@return);
                Assert.Equal(2, @return.Count());
            }
        }
    }
}
