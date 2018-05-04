﻿using Wrld.Space;
using Assets.Wrld.Scripts.Maths;
using System.Diagnostics;
using System;

namespace Wrld.Resources.IndoorMaps
{
    public class IndoorMapsApi
    {
        private IndoorMapsApiInternal m_apiInternal;

        public event Action OnIndoorMapEntered;
        public event Action OnIndoorMapExited;
        public event Action<int> OnIndoorMapFloorChanged;
        public event Action<string> OnIndoorMapEntityClicked;

        internal IndoorMapsApi(IndoorMapsApiInternal apiInternal)
        {
            m_apiInternal = apiInternal;

            m_apiInternal.OnIndoorMapEnteredInternal += () => { if (OnIndoorMapEntered != null) OnIndoorMapEntered(); };
            m_apiInternal.OnIndoorMapExitedInternal += () => { if (OnIndoorMapExited != null) OnIndoorMapExited(); };
            m_apiInternal.OnIndoorMapFloorChangedInternal += () => { if (OnIndoorMapFloorChanged != null) { var floorId = GetCurrentFloorId(); OnIndoorMapFloorChanged(floorId); } };
            m_apiInternal.OnIndoorMapEntityClickedInternal += entityIds => { if (OnIndoorMapEntityClicked != null) OnIndoorMapEntityClicked(entityIds); };
        }

        /// <summary>
        /// The factory that will be used when creating materials for interiors.  This can be replaced to allow
        /// the interior materials to be customized.
        /// </summary>
        public IIndoorMapMaterialFactory IndoorMapMaterialFactory
        {
            get { return m_apiInternal.IndoorMapMaterialFactory; }
            set { m_apiInternal.IndoorMapMaterialFactory = value; }
        }

        /// <summary>
        /// The texture fetcher that will be used when creating materials for interiors.  This can be replaced to allow
        /// the interior materials to be customized.
        /// </summary>
        public IIndoorMapTextureFetcher IndoorMapTextureFetcher
        {
            get { return m_apiInternal.IndoorMapTextureFetcher; }
            set { m_apiInternal.IndoorMapTextureFetcher = value; }
        }

        /// <summary>
        /// Try to enter the indoor map with the given ID.
        /// </summary>
        /// <param name="indoorMapId">This is the ID of the desired indoor map.</param>
        public void EnterIndoorMap(string indoorMapId)
        {
            m_apiInternal.EnterIndoorMap(indoorMapId);
        }

        /// <summary>
        /// Exit the current indoor map.
        /// </summary>
        public void ExitIndoorMap()
        {
            m_apiInternal.ExitIndoorMap();
        }


        /// <summary>
        /// Show the expanded indoor map view.
        /// </summary>
        public void ExpandIndoor()
        {
            m_apiInternal.ExpandIndoor();
        }

        /// <summary>
        /// Show the collapsed indoor map view.
        /// </summary>
        public void CollapseIndoor()
        {
            m_apiInternal.CollapseIndoor();
        }

        /// <summary>
        /// Gets the active indoor map.
        /// </summary>
        /// <returns>The active indoor map, or null if no indoor map is active.</returns>
        public IndoorMap GetActiveIndoorMap()
        {
            return m_apiInternal.GetActiveIndoorMap();
        }

        /// <summary>
        /// Gets the active floor of the current indoor map.
        /// </summary>
        /// <returns>The index of the active floor of the current indoor map, or -1 if no indoor map is active.</returns>
        public int GetCurrentFloorId()
        {
            return m_apiInternal.GetSelectedFloorId();
        }

        /// <summary>
        /// Sets the current floor shown in an indoor map.
        /// </summary>
        /// <param name="floorId">The ID of the floor to be selected.  This should match an element of the array returned by IndoorMap.FloorIds</param>
        public void SetCurrentFloorId(int floorId)
        {
            m_apiInternal.SetSelectedFloorId(floorId);
        }

        /// <summary>
        /// Moves up a number of floors in the current indoor map.
        /// </summary>
        /// <param name="numberOfFloors">The number of floors to move up.</param>
        public void MoveUpFloor(int numberOfFloors = 1)
        {
            m_apiInternal.MoveUpFloor(numberOfFloors);
        }

        /// <summary>
        /// Moves down a number of floors in the current indoor map.
        /// </summary>
        /// <param name="numberOfFloors">The number of floors to move down</param>
        public void MoveDownFloor(int numberOfFloors = 1)
        {
            m_apiInternal.MoveDownFloor(numberOfFloors);
        }

        /// <summary>
        /// Sets the interpolation value used in the expanded indoor view.
        /// </summary>
        /// <param name="dragParameter">The float value, in the range 0 .. number of floors - 1</param>
        public void SetIndoorFloorInterpolation(float dragParameter)
        {
            m_apiInternal.SetIndoorFloorInterpolation(dragParameter);
        }

        /// <summary>
        /// Highlights one or more indoor entities.
        /// </summary>
        /// <param name="ids">The IDs of the entities.</param>
        /// <param name="color">The color of the highlight.</param>
        /// <param name="indoorId">The ID of the indoor map which contains the entity ids. (Optional, the active indoor map will be used if no id is supplied)</param>
        public void SetIndoorEntityHighlights(string[] ids, UnityEngine.Color color, string indoorId = null)
        {
            m_apiInternal.SetEntityHighlights(ids, color, indoorId);
        }

        /// <summary>
        /// Highlights a single indoor entity.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <param name="color">The color of the highlight.</param>
        /// <param name="indoorId">The ID of the indoor map which contains the entity ids. (Optional, the active indoor map will be used if no id is supplied)</param>
        public void SetIndoorEntityHighlight(string id, UnityEngine.Color color, string indoorId = null)
        {
            SetIndoorEntityHighlights(new[] { id }, color, indoorId);
        }

        /// <summary>
        /// Clears the highlights from one or more indoor entities. If no ids are specified then all highlighted entities will be cleared.
        /// </summary>
        /// <param name="ids">The IDs of the entities.</param>
        /// <param name="indoorId">The ID of the indoor map which contains the entity ids. (Optional, the active indoor map will be used if no id is supplied)</param>
        public void ClearIndoorEntityHighlights(string[] ids = null, string indoorId = null)
        {
            m_apiInternal.ClearEntityHighlights(ids, indoorId);
        }

        /// <summary>
        /// Clears the highlights from a single indoor entity.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <param name="indoorId">The ID of the indoor map which contains the entity ids. (Optional, the active indoor map will be used if no id is supplied)</param>
        public void ClearIndoorEntityHighlight(string id, string indoorId = null)
        {
            ClearIndoorEntityHighlights(new[] { id }, indoorId);
        }
    }
}

