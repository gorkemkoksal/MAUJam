using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core._Common.Extensions
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> GetAllChildren(this Transform tra)
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < tra.childCount; i++)
            {
                list.Add(tra.GetChild(i));
            }

            return list;
        }

        public static List<Transform> GetAllChildrenAtDepth(this Transform tra, int depth)
        {
            List<Transform> result = new() {tra};
            for (int i = 0; i < depth; i++)
            {
                List<Transform> newList = new();
                foreach (Transform transform in result)
                {
                    newList = newList.Concat(transform.GetAllChildren()).ToList();
                }

                result = newList;
            }

            return result;
        }

        public static void SetLocalPosition(this Transform tra, float? x = null, float? y = null, float? z = null)
        {
            Vector3 localPosition = tra.localPosition;
            localPosition =
                new Vector3(x ?? localPosition.x, y ?? localPosition.y, z ?? localPosition.z);
            tra.localPosition = localPosition;
        }

        public static void SetPosition(this Transform tra, float? x = null, float? y = null, float? z = null)
        {
            Vector3 position = tra.position;
            position =
                new Vector3(x ?? position.x, y ?? position.y, z ?? position.z);
            tra.position = position;
        }

        public static void KillAllChildren(this Transform tra)
        {
            while (tra.childCount != 0)
            {
                Object.Destroy(tra.GetChild(0).gameObject);
            }
        }
    }
}