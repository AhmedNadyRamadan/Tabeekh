enum APIEndpoints {
  APIENDPOINT = '/api',

  // Auth End Points
  Auth = APIENDPOINT + '/Auth',
  Login = Auth + '/Login',
  Register = Auth + '/Register',

  //User End Points
  User = Auth + '/User',
  UpdateUser = Auth + '/UpdateUser',
  Photo = '/Photo',

  //Orders EndPoints
  Order = '/Order',
  addOrder = APIENDPOINT + '/Order',

  // Customer
  Customer = APIENDPOINT + '/Customer',

  //Review End points
  Reviews = '/Reviews',
  EReviews = APIENDPOINT + Reviews,
  AddChiefReview = Customer + '/chiefs',

  //Meal End point
  Meal = APIENDPOINT + '/Meal',
  suggestMeal = APIENDPOINT + '/Meal/RecommendFood',

  //Chief End points
  Chief = APIENDPOINT + '/Chief',
  AddMeal = Chief + '/Meals',

  //Category End points
  Category = APIENDPOINT + '/Category',

  //Home Lookups
  Ctop10 = Chief + '/top10',
  Mtop10 = Meal + '/top10',
  MOffers = Meal + '/bestOffers',
}

export default APIEndpoints;
