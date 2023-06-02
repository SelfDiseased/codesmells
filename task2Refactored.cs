int getSpeed()
{
  switch() {
    case !isTransport():
      return getManSpeed();
    case isCar:
      return getCarSpeed();
    case isPlane:
      return getPlanesAverageSpeed();
    case isBoat:
      return getBoatSpeed();
  }
}

int getPlanesAverageSpeed() {
  int result = 0;

  foreach (PlaneType plane in m_planes)
  {
    // @TODO clarify if we really can sum speeds of different planes
    result += getPlaneSpeed(plane);
  }

  return result / m_planes.Length;
}