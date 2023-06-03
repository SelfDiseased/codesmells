int getSpeed()
{
  bool isTransport = isTransport();

  switch() {
    case !isTransport:
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
    result += getPlaneSpeed(plane);
  }

  // @TODO check float type
  return result / m_planes.Length;
}