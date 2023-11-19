<script lang="ts">
  let usernameErrors:string[]=[];
  let emailErrors:string[]=[];
  let passErrors:string[]=[];

  async function onSubmit(e:any) {
    usernameErrors=[];
    emailErrors=[];
    passErrors =[];
    e.preventDefault(); // Prevent the default form submission behavior

    const formData =  new FormData(e.target); // Get the form element
    let json = Object.fromEntries(formData.entries());
    const endpoint = "http://localhost/api/Account/register";
    console.log(JSON.stringify(json));

    const res = await fetch(endpoint, {
			method: 'POST',
      headers: {
        'Content-Type': 'application/json'
        },
			body: JSON.stringify(json)
      })
      const data = await res.json();
        console.log(data);
        if (data.status == 400) {

          if (data.errors.Username != undefined)
           {
            usernameErrors = data.errors.Username as string[];
          }
          if (data.errors.Email != undefined)
           {
            emailErrors=data.errors.Email as string[];

          }
          if (data.errors.Password != undefined)
           {
            passErrors=data.errors.Password as string[];
            }
        }
        }


</script>\

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
        <form class="card-body" on:submit|preventDefault={onSubmit}>
          <div class="form-control">
          <label for="loginusername" class="label">
            <span class="label-text">Username</span>
          </label>
          <input name="username" id="loginusername" placeholder="Username" class="input input-bordered" />
          {#each usernameErrors as error}
            <li class="text-error">{error}</li><br/>
          {/each}
        </div>
          <div class="form-control">
            <label for="loginemail" class="label">
              <span class="label-text">Email</span>
            </label>
            <input name="email" id="loginemail" placeholder="email" class="input input-bordered" />
            {#each emailErrors as error}
              <li class="text-error">{error}</li><br/>
            {/each}
          </div>
          <div class="form-control">
            <label for="loginpassword" class="label">
              <span class="label-text">Password</span>
            </label>
            <input name="password" id="loginpassword" type="password" placeholder="password" class="input input-bordered" />
              {#each passErrors as error}
                <li class="text-error">{error}</li><br/>
              {/each}
          </div>
          <div class="form-control mt-6">
            <button class="btn btn-primary" type="submit">Login</button>
          </div>
        </form>
      </div>
    </div>
  </div>