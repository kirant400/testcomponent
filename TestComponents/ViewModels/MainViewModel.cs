using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace TestComponents.ViewModels;
public class Conference
{
    public string ConferenceName { get; set; }

    public ObservableCollection<Team> Teams { get; }
        = new ObservableCollection<Team>();

}
public class Team
{
    public string TeamName { get; set; }

    public ObservableCollection<Person> Players { get; }

    public Team()
    {
        Players = new ObservableCollection<Person>();
    }
}
public class Person
{
    public string Name { get; set; }
}
public class Node
{
    public ObservableCollection<Node>? SubNodes { get; }
    public string Title { get; }

    public Node(string title)
    {
        Title = title;
    }

    public Node(string title, ObservableCollection<Node> subNodes)
    {
        Title = title;
        SubNodes = subNodes;
    }
}
public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public ObservableCollection<Conference> League { get; }

    public ObservableCollection<Node> Nodes { get; }

    public MainViewModel()
    {
        League = new ObservableCollection<Conference>(FillLeague());
        Nodes = new ObservableCollection<Node>
            {
                new Node("Animals", new ObservableCollection<Node>
                {
                    new Node("Mammals", new ObservableCollection<Node>
                    {
                        new Node("Lion"),
                        new Node("Cat"),
                        new Node("Zebra")
                    })
                })
            };
    }
    private List<Conference> FillLeague()
    {
        return new List<Conference>()
    {
        new Conference()
    {
        ConferenceName = "Eastern",
        Teams =
        {
            
            new Team()
            {
                TeamName = "Eastern Team A",
                Players =
                {
                    new Person() { Name = "Player 1"},
                    new Person() { Name = "Player 2"},
                    new Person() { Name = "Player 3"}
                }
            },
            new Team()
            {
                TeamName = "Eastern Team B"
            },
            new Team()
            {
                TeamName = "Eastern Team C"
            }
        }
    },
    new Conference()
    {
        ConferenceName = "Western",
        Teams =
        {
            new Team()
            {
                TeamName = "Western Team A"
            },
            new Team()
            {
                TeamName = "Western Team B"
            },
            new Team()
            {
                TeamName = "Western Team C"
            }
        }
    }
    };
    }
}
