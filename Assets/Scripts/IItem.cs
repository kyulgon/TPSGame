using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem // 상속해서 쓰는 거고 Use메서드를 구현해야함
{
    void Use(GameObject target);
}
