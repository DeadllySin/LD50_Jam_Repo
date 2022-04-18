using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Trace_Utils
    
{
    /// <summary>
    /// Fun��o que serve para testar se um objeto "v�" o outro, ou seja se n�o h� obst�culos entre dois objetos
    /// </summary>
    /// <param name="positionFrom">Transform do objeto inicial</param>
    /// <param name="positionTo">Transform do objeto final</param>
    /// <param name="tag">Opcional: tag do objeto a ver</param>
    /// <param name="fov">Opcional: campo de vis�o</param>
    /// <param name="MaxDistance">Opcional: dist�ncia m�xima</param>
    /// <returns></returns>
    public static bool CanYouSeeThis(Transform positionFrom, Transform positionTo, string tag = null, float fov = 0, float MaxDistance = 0)
    {
        RaycastHit hit;
        if (fov == 360) fov = 0;
        //testa distancia
        var distance = Vector3.Distance(positionFrom.position, positionTo.position);
        if (MaxDistance > 0 && distance > MaxDistance)
        {
            //Debug.Log("Muito longe");
            return false;  //muito longe
        }
        //dire��o
        var rayDirection = positionTo.position - positionFrom.position;
        Debug.DrawRay(positionFrom.position, rayDirection, Color.blue);
        if (Physics.Raycast(positionFrom.position, rayDirection, out hit))
        {
            //Debug.Log("hit " + hit.transform.name);
            //n�o testa tag nem fov
            if (string.IsNullOrEmpty(tag) && fov == 0) return (hit.transform.gameObject == positionTo.gameObject);

            //Debug.Log("hit tag " + hit.transform.tag);
            //testa tag n�o testa fov
            if (tag != null && fov == 0) return hit.transform.tag.Equals(tag);

            //c�lcula angulo
            float angle = Vector3.Angle(positionTo.position - positionFrom.position, positionFrom.forward);
            //Debug.Log("Angulo " + angle);

            //testa fov n�o testa tag
            if (tag == null && fov != 0)
            {
                if (angle <= fov && (hit.transform.gameObject == positionTo.gameObject))
                    return true;
                else
                    return false;
            }

            //testa tag e fov
            if (hit.transform.tag.Equals(tag) && angle <= fov && (hit.transform.gameObject == positionTo.gameObject))
                return true;
            return false;// (hit.transform.position == positionTo);
        }
        return false;
    }
    /// <summary>
    /// Fun��o que devolve um vetor com os componentes dos filhos sem o pai
    /// A fun��o que existe do Unity devolve sempre o pai
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="obj">Parent game object</param>
    /// <returns>Devolve um vetor com os componentes dos filhos sem incluir o pai</returns>
    public static T[] GetComponentsInChildWithoutRoot<T>(GameObject obj) where T : Component
    {
        List<T> tList = new List<T>();
        if (obj == null) return null;
        foreach (Transform child in obj.transform)
        {
            T[] scripts = child.GetComponentsInChildren<T>();
            if (scripts != null)
            {
                foreach (T sc in scripts)
                    tList.Add(sc);
            }
        }
        return tList.ToArray();
    }

}