import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: "", loadComponent: () => import("./features/Guest/home/home.component").then(m => m.HomeComponent) },  
    { path: "login", loadComponent: () => import("./features/Auth/login/login.component").then(m => m.LoginComponent) },  
    { path: "register", loadComponent: () => import("./features/Auth/register/register.component").then(m => m.RegisterComponent) },  
    { path: "chiefs", loadComponent: () => import("./features/Guest/chiefs/chiefs.component").then(m => m.ChiefsComponent) },  
    { path: "meals", loadComponent: () => import("./features/Guest/meals/meals.component").then(m => m.MealsComponent) },  
    { path: "meal/:id", loadComponent: () => import("./features/Guest/meal/meal.component").then(m => m.MealComponent) },  
    { path: "cart", loadComponent: () => import("./features/Customer/cart/cart.component").then(m => m.CartComponent) },  
    { path: "orders", loadComponent: () => import("./features/Delivery/orders/orders.component").then(m => m.OrdersComponent) },
    { path: "account", loadComponent: () => import("./features/Customer/account/account.component").then(m => m.AccountComponent) },
    { path: "account/update", loadComponent: () => import("./features/Customer/account/update-account/update-account.component").then(m => m.UpdateAccountComponent) },
    { path: "chief/dashboard", loadComponent: () => import("./features/Chief/chief-dashboard/chief-dashboard.component").then(m => m.ChiefDashboardComponent) },  
    { path: "chief/add-meal", loadComponent: () => import("./features/Chief/add-meal/add-meal.component").then(m => m.AddMealComponent) },  
    { path: "chief/:id", loadComponent: () => import("./features/Guest/chief/chief.component").then(m => m.ChiefComponent) }, 
    {path: "**", redirectTo: "/", pathMatch: "full" },
];
