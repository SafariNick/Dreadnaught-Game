using UnityEngine;
[RequireComponent(typeof(Animator))]

// private - non accessible from other scripts, only accessible from this class - if you want to keep a variable private and not accessible from other scripts, this is the way to go. The accessiblity is limited to this class only and childern of this class will not be able to access it.
// protected - private for the entire inheritance hierarchy of this class - if you want to keep a variable private but accessible from child classes, this is the way to go. The accessiblity is limited to this class and its children only.
// public - a variable that is publicly accessible from other scripts - if an object has a instance of this class (via reference from another class or via GetComponent)
// this can be a problem if you want to change the variable but don't want other scripts to be able to change it. Tracking down bugs can be difficult if you have many
// scripts that can change the variable.

public class Enemy : MonoBehaviour

{

}
