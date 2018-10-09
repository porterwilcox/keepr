<template>
    <v-card @mouseenter="showActions(keep.id)" @mouseleave="hideActions(keep.id)">
        <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text">
            <v-layout column fill-height>
                <v-spacer></v-spacer>
                <v-card-title class="img-title">{{keep.name}}</v-card-title>
            </v-layout>
        </v-img>
        <v-card-title v-if="keep.img.length < 1">{{keep.name}}</v-card-title>
        <v-card-text>{{keep.description}}</v-card-text>
        <v-card-actions :id="keep.id" class="buttons"> 
            <view-keep :keep="keep"/>
            <v-btn flat fab small color="amber darken-4">keep</v-btn> 
            <v-speed-dial v-model="shareExpand" direction="right" transition="slide-y-reverse-transition">
                <v-btn slot="activator" v-model="shareExpand" color="amber darken-4" fab flat small>
                    <v-icon>fa-share</v-icon>
                    <v-icon>close</v-icon>
                </v-btn>
                <v-btn fab small color="blue">
                    <v-icon>fab fa-facebook-f</v-icon>
                </v-btn>
                <v-btn fab small color="#e62774">
                    <v-icon>fab fa-instagram</v-icon>
                </v-btn>
                <v-btn fab small color="black" class="white--text">
                    <v-icon>fab fa-twitter</v-icon>
                </v-btn>
            </v-speed-dial>
            <v-spacer></v-spacer>
            <v-btn v-if="keep.userId == user.id" @click="deleteKeep(keep.id, keep.userId)" fab flat small color="amber darken-4">
                    <v-icon>fas fa-trash-alt</v-icon>
                </v-btn>
        </v-card-actions>
    </v-card>     
</template>
<script>
import ViewKeep from '@/components/ViewKeep'
export default {
    name: "keeps",
    data() {
        return {
        shareExpand: false
        }
    },
    props: ["keep"],
    components: {
        ViewKeep
    },
    computed: {
        user() {
            return this.$store.state.user
        }
    },
    methods: {
        showActions(id) {
            $(`#${id}`).css({"display": "flex"})
        },
        hideActions(id) {
            $(`#${id}`).css({"display": "none"})
            this.shareExpand = false
        },
        deleteKeep(keepId, authorId) {
            let k = {
                id: keepId,
                userId: authorId,
                sendingUserId: this.user.id
            }
            this.$store.dispatch("deleteKeep", k)
        }
    }    
}
</script>
<style scoped>
.img-title {
    text-shadow: 0 0 5px black;
    font-size: 120%;
}
.buttons {
    display: none;
}
@media (hover: none) {
    .buttons {
        display: flex;
    }
}
</style>
