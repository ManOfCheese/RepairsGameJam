using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer List", menuName = "Customer List")]
public class Customers : ScriptableObject
{
    public List<Customer> CustomerList;
}

[System.Serializable]
public class Customer
{
    public string CustomerName;
    public GameObject Model;
    public List<string> Exclamations;
}
