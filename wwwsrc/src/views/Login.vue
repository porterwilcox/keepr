<template>
    <v-content id="h">
        <v-container fill-height>
            <v-layout justify-center align-center >
                <v-flex sm8 md4>
                    <div class="login">
                        <v-card v-if="loginForm">
                            <v-toolbar card color="cyan lighten-3">
                                <v-icon color="grey lighten-5" large>fab fa-korvue</v-icon>
                                <v-toolbar-title>Login</v-toolbar-title>
                                 <v-spacer></v-spacer>
                                <v-btn flat round @click="loginForm = !loginForm" class="white--text">
                                    or sign up
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="loginUser" class="ma-3">
                                <v-text-field prepend-icon="mail" color="cyan lighten-3" single-line clearable v-model="creds.email" placeholder="email"></v-text-field>
                                <v-text-field type="password" prepend-icon="lock" color="cyan lighten-3" single-line clearable v-model="creds.password" placeholder="password"></v-text-field>
                                <v-btn type="submit" v-if="creds.password.length > 5" color="cyan lighten-3">login</v-btn>
                            </form>
                        </v-card>
                        <v-card v-else>
                            <v-toolbar card>
                                <v-icon color="cyan lighten-3" large>fab fa-korvue</v-icon>
                                <v-toolbar-title>Create Accout</v-toolbar-title>
                                <v-spacer></v-spacer>
                                <v-btn flat round @click="loginForm = !loginForm" class="cyan--text">
                                    or login
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="register" class="ma-3">
                                <v-text-field prepend-icon="person" color="cyan lighten-3" single-line clearable v-model="newUser.username" placeholder="username"></v-text-field>
                                <v-text-field prepend-icon="mail" color="cyan lighten-3" single-line clearable v-model="newUser.email" placeholder="email"></v-text-field>
                                <v-text-field type="password" prepend-icon="lock" color="cyan lighten-3" single-line clearable v-model="newUser.password" placeholder="password"></v-text-field>
                                <v-text-field v-if="newUser.password.length > 5" @input="passwordConfirm()" type="password" prepend-icon="check" color="cyan lighten-3" single-line clearable v-model="newUser.password2" placeholder="confirm password"></v-text-field>
                                <v-btn type="submit" v-if="passwordsMatch" color="cyan lighten-3">register</v-btn>
                            </form>
                        </v-card>
                    </div>
                    <v-layout pt-3>
                        <router-link :to="{name: 'home', params: {browsing: true}}" class="cyan--text">I want to browse keepr without an account.</router-link>
                    </v-layout>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            //checks for valid session
            this.$store.dispatch("authenticate");
            if (this.signUp == "true") {
                this.loginForm = false;
            }
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: "",
                    password2: ""
                },
                passwordsMatch: false
            };
        },
        props: ["signUp"],
        methods: {
            passwordConfirm() {
                if (this.newUser.password == this.newUser.password2) {
                    return this.passwordsMatch = true;
                }
                this.passwordsMatch = false
            },
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>
<style scoped>
#h {
    height: 100vh;
}
</style>
