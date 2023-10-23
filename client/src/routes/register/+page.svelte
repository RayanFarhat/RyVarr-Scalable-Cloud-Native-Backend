<div class="hero min-h-screen pt-12 bg-base-200">
    <div class="hero-content flex-col lg:flex-row-reverse">
      <div class="text-center lg:text-left">
        <h1 class="text-5xl font-bold">Register now!</h1>
        <p class="py-6">To Access our products, You must sign up first. So we can identify you. </p>
        <div class="flex justify-center items-center bg-base-200">
            <p>Already have an account? Sign in here</p>
            <a href="/login">
                <button class="btn btn-outline btn-primary ml-3">Sign In</button>
            </a>
        </div>
      </div>
      <div class="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
        <form class="card-body">
          <div class="form-control">
          <label for="loginusername" class="label">
            <span class="label-text">Username</span>
          </label>
          <input id="loginusername" placeholder="Username" class="input input-bordered" required />
        </div>
          <div class="form-control">
            <label for="loginemail" class="label">
              <span class="label-text">Email</span>
            </label>
            <input id="loginemail" type="email" placeholder="email" class="input input-bordered" required />
          </div>
          <div class="form-control">
            <label for="loginpassword" class="label">
              <span class="label-text">Password</span>
            </label>
            <input id="loginpassword" type="password" placeholder="password" class="input input-bordered" required />
          </div>
          <div class="form-control mt-6">
            <button class="btn btn-primary">Login</button>
          </div>
        </form>
      </div>
    </div>
  </div>